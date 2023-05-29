
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct InteractableProperties
{
    public DropType Type;
    public float Health;
    public float Damage;
    public int Level;
    public bool IsObstacle;
    public bool IsSlowTrap;
    public bool IsFireTrap;
}

public class InteractableController : MonoBehaviour, IHit
{
    [SerializeField] private GameConfig _gameConfig;
    private bool _canBeHit = true;
    private bool _isOnFire;
    private bool _wasInitialized;
    private InteractableProperties _properties;
    private NavMeshObstacle _navMeshObstacle;
    private Vector3 _originalScale;

    public void Setup(InteractableProperties properties)
    {
        _properties = properties;
        _wasInitialized = true;

        if (_properties.IsObstacle)
        {
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
            _navMeshObstacle.enabled = true;
        } 
        else
        {
            transform.localScale = new Vector3(properties.Level, transform.localScale.y, properties.Level);
        }

        _originalScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (!_wasInitialized) return;
        
        if (_isOnFire)
        {
            OnHit(new ProjectileProperties()
            {
                Damage = _gameConfig.FireDamage
            });
        }

        if (_properties.Health <= 0 && _properties.IsObstacle)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;
        
        var listeners = collision.collider.GetComponents<IHit>();
        foreach (var listener in listeners)
        {
            listener.OnHit(new ProjectileProperties()
            {
                HasSlow = _properties.IsSlowTrap,
                CanIgnite = _properties.IsFireTrap
            });
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;
        
        var listeners = other.GetComponents<IHit>();
        foreach (var listener in listeners)
        {
            listener.OnHit(new ProjectileProperties()
            {
                Damage = _properties.Damage,
                HasSlow = _properties.IsSlowTrap,
                CanIgnite = _properties.IsFireTrap
            });
        }
    }

    public void OnHit(ProjectileProperties properties)
    {
        if (!_canBeHit || !_wasInitialized) return;

        if (properties.HasSlow)
        {
            if (_properties.IsFireTrap)
            {
                _properties.IsFireTrap = false;
            }
            else
            {
                _isOnFire = false;
            }
        } 

        if (properties.CanIgnite)
        {
            if (_properties.IsSlowTrap)
            {
                _properties.IsSlowTrap = false;
            }
            else
            {
                _isOnFire = true;
            }
        }

        if (!_properties.IsFireTrap && !_properties.IsSlowTrap && !_properties.IsObstacle)
        {
            Destroy(gameObject);
            return;
        }

        _properties.Health -= properties.Damage;

        if (properties.Damage > 0)
        {
            transform.DOKill();
            transform.localScale = _originalScale;
            transform.DOPunchScale(new Vector3(1.1f * _originalScale.x, 1.1f * _originalScale.y, 1.1f * _originalScale.z), 0.2f);
        }
        StartCoroutine(WaitHit());
    }

    IEnumerator WaitHit()
    {
        _canBeHit = false;
        yield return new WaitForSeconds(_gameConfig.InvincibilityTime);
        _canBeHit = true;
    }
}