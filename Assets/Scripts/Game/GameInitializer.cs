
using System;
using UnityEngine;
using UnityEngine.AI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform _dropTarget;
    [SerializeField] private WaveConfig WaveConfig;
    private void Awake()
    {
        Application.targetFrameRate = _frameRate;
        _playerData.CreateInstance();
        WaveConfig.ActualWave = -1;
    }

    private void Start()
    {
        _gameConfig.State = GameStates.Preparation;
        _gameConfig.OnStateChanged?.Invoke();
        _gameConfig.DropTarget = _dropTarget;
    }
}
