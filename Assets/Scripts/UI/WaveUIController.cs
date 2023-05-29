
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private CanvasGroup _canvas;

    private void Update()
    {
        if (_waveConfig.ActualWave <= 0) return;

        if (_gameConfig.State != GameStates.Combat)
        {
            _canvas.alpha = 0;
            return;
        }

        _canvas.alpha = 1;
        _text.text = $"Wave: #{_waveConfig.ActualWave + 1}";
    }
}
