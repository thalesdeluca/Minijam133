
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public struct PhaseText
{
    public GameStates State;
    public string Text;
}

public class PhaseUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private List<PhaseText> _phases;
    [SerializeField] private GameConfig _gameConfig;
    
    private void OnStateChanged()
    {
        _text.text = _phases[(int)_gameConfig.State].Text;
        _text.transform.localScale = Vector3.one;
        _text.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f);
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
