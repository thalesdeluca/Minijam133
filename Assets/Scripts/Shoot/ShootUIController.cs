using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private PlayerData _playerData;

    private void FixedUpdate()
    {
        _ammoText.text = $"Ammo: {_playerData.Instance.CurrentAmmo}/{_playerData.Instance.TotalAmmo}";
    }
}