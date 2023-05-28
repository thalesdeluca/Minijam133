
using System;
using TMPro;
using UnityEngine;

public class TimeUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private GameConfig _gameConfig;
    
    private void Update()
    {
        if (_gameConfig.State != GameStates.Combat)
        {
            _group.alpha = 0f;
            return;
        }
        
        _group.alpha = 1f;
        _timeText.text = $"Time Remaining: {_waveConfig.WaveTime.ToString("00")}s";
    }
}
