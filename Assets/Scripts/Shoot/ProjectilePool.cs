
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectilePrefab;
    private static ProjectilePool _instance;

    public static ProjectilePool Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    private Queue<ProjectileController> _projectilePool = new Queue<ProjectileController>();

    public ProjectileController SpawnProjectile(ProjectileProperties properties)
    {
        if (_projectilePool.Count <= 0)
        {
            _projectilePool.Enqueue(Instantiate(_projectilePrefab));
        }
        
        var projectile = _projectilePool.Dequeue();
        projectile.gameObject.SetActive(true);
        projectile.Setup(properties);
        return projectile;
    }
    
    public void DestroyProjectile(ProjectileController projectile)
    {
        if (projectile == null) return;

        projectile.gameObject.SetActive(false);
        _projectilePool.Enqueue(projectile);
    }
}
