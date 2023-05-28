
using System.Collections;
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

    public void Setup(float health)
    {
        Health = health;
    }
    
    public void OnHit(ProjectileProperties properties)
    {
        if (Health <= 0 || !_canBeHit) return;
        
        if (properties.HasSlow)
        {
            _isOnFire = false;
        } 

        if (properties.CanIgnite)
        {
            _isOnFire = true;
        }
        
        Health -= properties.Damage;
    }
    
    
    private void FixedUpdate()
    {
        if (_isOnFire)
        {
            OnHit(new ProjectileProperties()
            {
                Damage = _gameConfig.FireDamage
            });
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    IEnumerator WaitHitStop()
    {
        _canBeHit = false;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(_gameConfig.HitStopTime);
        Time.timeScale = 1;
        yield return new WaitForSeconds(_gameConfig.InvincibilityTime);
        _canBeHit = true;
    }
}
