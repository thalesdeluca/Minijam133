
using System;
using Unity.VisualScripting;
using UnityEngine;

public struct ProjectileProperties
{
    public Vector3 Position;
    public Vector3 Direction;
    public float BulletSpeed;
    public float Damage;
    public bool HasSlow;
    public bool CanIgnite;
    public bool CanSmoke;
}

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    private ProjectileProperties _properties;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(ProjectileProperties properties)
    {
        _properties = properties;
        transform.position = properties.Position;
        transform.rotation = Quaternion.LookRotation(_properties.Direction);
        _trail.Clear();
    }

    public void Update()
    {
        if (!gameObject.activeSelf) return;
        
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.x > 1)
        {
            ProjectilePool.Instance.DestroyProjectile(this);
            return;
        }

        _rigidbody.velocity = _properties.BulletSpeed * _properties.Direction.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProjectilePool.Instance.DestroyProjectile(this);
        if (collision.collider == null) return;

        foreach (var listener in collision.collider.GetComponents<IHit>())
        {
            listener.OnHit(_properties);
        }
    }
}
