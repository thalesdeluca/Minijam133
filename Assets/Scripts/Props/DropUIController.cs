
using System;
using TMPro;
using UnityEngine;

public class DropUIController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private DropType _dropType;
    [SerializeField] private GameConfig _gameConfig;

    private void FixedUpdate()
    {
        if (_gameConfig.Player == null) return;
        
        _playerData.Instance.Drops.TryGetValue(_dropType, out var amount);
        amountText.text = $"x{amount}";
    }
}
