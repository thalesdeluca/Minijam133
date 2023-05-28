using System;
using TMPro;
using UnityEngine;

public class AttackUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private PlayerData _playerData;
    
    private void FixedUpdate()
    {
        if (_playerData.Instance == null) return;
        
        _attackText.text = $"Attack: {_playerData.Instance.Attack}";
    }
}