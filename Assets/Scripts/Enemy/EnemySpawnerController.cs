using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private EnemyList _enemies;
    [SerializeField] private List<Transform> Spawns;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private GameConfig _gameConfig;

    private float _spawnTime;

    private void Awake()
    {
        _spawnTime = _waveConfig.Wave.SpawnDelay;
    }

    private void FixedUpdate()
    {
        if (_gameConfig.State != GameStates.Combat) return;

        _spawnTime -= Time.fixedTime;
        if (_spawnTime <= 0)
        {
            SpawnEnemy();
            _spawnTime = _waveConfig.Wave.SpawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        if (_gameConfig.Player == null) return;
        
        var gate = Random.Range(0, _waveConfig.Wave.GateNumber);
        var enemyIndex = Random.Range(0, _enemies.Enemies.Count);

        var obj = Instantiate(_enemies.Enemies[enemyIndex], Spawns[gate].position, Quaternion.identity);
        obj.transform.rotation = Quaternion.LookRotation((_gameConfig.Player.position - obj.transform.position).normalized);
    }

}
