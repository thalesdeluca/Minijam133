
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private EnemyData _baseData;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private DropList _dropList;
        
    private HealthController _healthController;
    private NavMeshAgent _agent;
    private EnemyData _data;
    private bool _isDead;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _agent = GetComponent<NavMeshAgent>();
        
        _data = _baseData.CreateInstance();
        _healthController.Setup(_data.Health * _waveConfig.Wave.DifficultyMultiplier, _gameConfig.InvincibilityTime);
        _healthController.OnDie.AddListener(OnDie);
    }

    private void OnDie()
    {
        _isDead = true;
        StopAllCoroutines();
        foreach (var dropType in _data.Drops)
        {
            var drop = Instantiate<DropController>(_dropList.DropPrefab, transform.position, Quaternion.identity);
            var dropData = _dropList.GetProp(dropType);
            drop.Setup(dropData);
        }
    }

    private void Update()
    {
        if (_gameConfig.Player == null || _isDead) return;

        if (_gameConfig.State != GameStates.Combat)
        {
            _isDead = true;
            StopAllCoroutines();
            Destroy(gameObject);
            return;
        }

        _agent.speed = _data.Speed * _waveConfig.Wave.DifficultyMultiplier;
        _agent.SetDestination(_gameConfig.Player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null || _isDead) return;

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
        if (collision.collider == null || _isDead) return;

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
