
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private HealthController _healthController;
    [SerializeField] private PlayerData _playerData;
    
    private void Awake()
    {
        _gameConfig.Player = transform;
        _healthController.OnDie.AddListener(OnPlayerDie);
    }

    private void Update()
    {
        if (_healthController == null || _playerData.Instance) return;

        _playerData.Instance.Health = _healthController.Health;
    }

    void OnPlayerDie()
    {
        _gameConfig.State = GameStates.Death;
        _gameConfig.OnStateChanged?.Invoke();
        _healthController.OnDie.RemoveListener(OnPlayerDie);
    }
}
