
using System;
using UnityEngine;
using UnityEngine.AI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameConfig _gameConfig;
    private void Awake()
    {
        Application.targetFrameRate = _frameRate;
        _playerData.CreateInstance();
    }

    private void Start()
    {
        _gameConfig.State = GameStates.Preparation;
        _gameConfig.OnStateChanged?.Invoke();
    }
}
