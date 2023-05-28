
using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MergeUIController : MonoBehaviour
{
    [SerializeField] private DropList _dropList;
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private RectTransform _container;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private MergeCombinationController _mergePrefab;
    [SerializeField] private Button _prepareButton;

    private void Awake()
    {
        _prepareButton.onClick.AddListener(OnPrepareClicked);
    }

    private void Start()
    {
        foreach (var combination in _dropList.Combinations)
        {
            var obj = Instantiate(_mergePrefab, _container);
            obj.Setup(combination);
        }
    }

    private void Update()
    {
        if (_gameConfig.State != GameStates.Fuse)
        {
            _group.alpha = 0;
            _group.blocksRaycasts = false;
            _group.interactable = false;
            return;
        }
    }

    void OnPrepareClicked()
    {
        _gameConfig.State = GameStates.Preparation;
        _gameConfig.OnStateChanged?.Invoke();
    }

    void OnStateChanged()
    {
        if (_gameConfig.State != GameStates.Fuse) return;
        
        _group.DOFade(1,0.75f).From(0).OnComplete(() =>
        {
            _group.blocksRaycasts = true;
            _group.interactable = true;
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
