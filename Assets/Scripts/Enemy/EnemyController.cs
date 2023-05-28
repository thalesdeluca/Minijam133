
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private EnemyData _baseData;
    [SerializeField] private WaveConfig _waveConfig;
        
    private HealthController _healthController;
    private NavMeshAgent _agent;
    private EnemyData _data;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _agent = GetComponent<NavMeshAgent>();
        
        _data = _baseData.CreateInstance();
        _healthController.Setup(_data.Health * _waveConfig.Wave.DifficultyMultiplier);
    }

    private void Update()
    {
        if (_gameConfig.Player == null) return;

        _agent.speed = _data.Speed * _waveConfig.Wave.DifficultyMultiplier;
        _agent.SetDestination(_gameConfig.Player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;

        foreach (var listener in collision.collider.GetComponents<IHit>())
        {
            listener.OnHit(new ProjectileProperties()
            {
                Damage = _data.Damage * _waveConfig.Wave.DifficultyMultiplier,
                HasSlow = _data.CanSlow,
                CanIgnite = _data.CanIgnite
            });
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == null) return;

        foreach (var listener in collision.collider.GetComponents<IHit>())
        {
            listener.OnHit(new ProjectileProperties()
            {
                Damage = _data.Damage * _waveConfig.Wave.DifficultyMultiplier,
                HasSlow = _data.CanSlow,
                CanIgnite = _data.CanIgnite
            });
        }
    }
}
