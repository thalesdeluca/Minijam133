
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HealthController : MonoBehaviour, IHit
{
    [SerializeField] public float Health = 100;
    [SerializeField] private GameConfig _gameConfig;

    public UnityEvent OnDie;

    private bool _canBeHit = true;
    private bool _isOnFire = false;
    private float _invincibilityTime;
    private bool _isDead;

    public void Setup(float health, float invincibilityTime)
    {
        Health = health;
        _invincibilityTime = invincibilityTime;
    }
    
    public void OnHit(ProjectileProperties properties)
    {
        if (Health <= 0 || !_canBeHit || _isDead) return;
        
        if (properties.HasSlow)
        {
            _isOnFire = false;
        } 

        if (properties.CanIgnite)
        {
            _isOnFire = true;
        }
        
        Health -= properties.Damage;

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
        StartCoroutine(WaitHitStop());
    }
    
    
    private void FixedUpdate()
    {
        if(_isDead) return;
        
        if (_isOnFire)
        {
            OnHit(new ProjectileProperties()
            {
                Damage = _gameConfig.FireDamage
            });
        }

        if (Health <= 0)
        {
            _isDead = true;
            transform.DOKill();
            OnDie?.Invoke();
            gameObject.SetActive(false);
            StartCoroutine(WaitKill());
        }
    }

    IEnumerator WaitKill()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    

    IEnumerator WaitHitStop()
    {
        _canBeHit = false;
        
        // Time.timeScale = 0.1f;
        // yield return new WaitForSeconds(_gameConfig.HitStopTime);
        // Time.timeScale = 1;
        
        yield return new WaitForSeconds(_invincibilityTime);
        _canBeHit = true;
    }
}
