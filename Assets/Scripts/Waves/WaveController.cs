
using System;
using Unity.AI.Navigation;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private NavMeshSurface _surface;
    private void FixedUpdate()
    {
        _surface.BuildNavMesh();
    }

    private void Update()
    {
        if (_gameConfig.State != GameStates.Combat) return;

        _waveConfig.WaveTime -= Time.deltaTime;
        if (_waveConfig.WaveTime <= 0)
        {
            _gameConfig.State = GameStates.Fuse;
            _gameConfig.OnStateChanged?.Invoke();
        }
    }

    private void OnStateChanged()
    {
        if (_gameConfig.State == GameStates.Combat)
        {
            _waveConfig.ActualWave++;
            _waveConfig.WaveTime = _waveConfig.Wave.TotalTime;
            _waveConfig.OnWaveStarted?.Invoke();
        }
    }

    private void OnEnable()
    {
        _gameConfig.OnStateChanged.AddListener(OnStateChanged);
    }

    private void OnDisable()
    {
        _gameConfig.OnStateChanged.RemoveListener(OnStateChanged);

    }
}
