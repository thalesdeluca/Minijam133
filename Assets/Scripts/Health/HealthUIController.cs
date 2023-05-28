
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private PlayerData _playerData;

    private void FixedUpdate()
    {
        _healthText.text = $"Health: {_playerData.Instance.Health}";
    }
}
