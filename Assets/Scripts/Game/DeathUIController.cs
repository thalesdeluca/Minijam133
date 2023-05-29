
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUIController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private Button _restartButton;
    
    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartClick);
        _group.interactable = false;
        _group.blocksRaycasts = false;
        _group.alpha = 0;
    }

    private void OnRestartClick()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    private void OnStateChanged()
    {
        if (_gameConfig.State != GameStates.Death) return;

        _waveText.text = $"Wave #{_waveConfig.ActualWave + 1}";
        
        _group.DOFade(1, 2f).From(0).OnComplete(() =>
        {
            _group.interactable = true;
            _group.blocksRaycasts = true;
        });
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
