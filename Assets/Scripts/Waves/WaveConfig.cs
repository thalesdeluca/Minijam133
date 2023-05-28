
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Wave/WaveConfig", fileName = "WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public int ActualWave;
    public float WaveTime;
    public List<WaveSettings> Settings;
    public WaveSettings Wave => Settings.Count < ActualWave ? Settings[ActualWave] : Settings.Last();
    public UnityEvent OnWaveStarted;
}

[Serializable]
public struct WaveSettings
{
    public int GateNumber;
    public float SpawnDelay;
    public float DifficultyMultiplier;
    public float TotalTime;
}