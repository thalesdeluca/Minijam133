using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Slot
{
    public Image Image;
    public Outline Outline;
}

public class HotbarUIController : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private DropList _dropList;

    private void FixedUpdate()
    {
        for (int i = 0; i < _playerData.MaxInventorySize; i++)
        {
            var slot = _slots[i];
            slot.Outline.enabled = false;
            
            var data = _playerData.Instance.PropsAvailable[i];

            if (data == null)
            {
                slot.Image.sprite = null;
                slot.Image.color = Color.clear;
                continue;
            }

            slot.Image.sprite = _dropList.GetInteractable(data.Value.Type).Icon;
            slot.Image.color = Color.white;
            
        }

        _slots[_playerData.Instance.ItemIndexSelected].Outline.enabled = true;
    }
}
