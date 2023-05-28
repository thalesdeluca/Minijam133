
using System;
using TMPro;
using UnityEngine;

public class ReloadUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private PlayerData _playerData;
    
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        _group.alpha = _playerData.Instance.IsReloading ? 1 : 0;
    }
}
