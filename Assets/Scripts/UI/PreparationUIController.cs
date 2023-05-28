
using System;
using UnityEngine;
using UnityEngine.UI;

public class PreparationUIController : MonoBehaviour
{
    [SerializeField] private Button _nextPhaseButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameConfig _gameConfig;

    private void Awake()
    {
        _nextPhaseButton.onClick.AddListener(OnNextPhaseClicked);
    }

    private void FixedUpdate()
    {
        if (_gameConfig.State != GameStates.Preparation)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            return;
        }
        
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    void OnNextPhaseClicked()
    {
        _gameConfig.State = GameStates.Combat;
        _gameConfig.OnStateChanged?.Invoke();
    }
}
