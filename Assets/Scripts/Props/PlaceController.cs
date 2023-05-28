
using System;
using System.Linq;
using UnityEngine;

public class PlaceController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private float _offset;
    [SerializeField] private DropList _dropList;
    [SerializeField] private Material _previewMaterial;
    
    private GameObject _interactableHold;
    private MeshRenderer _interactableMeshRenderer;
    private Material _previousMaterial;
    private int _lastIndexSelected = -1;
    private bool _wasTrigger;
    private Collider _collider;

    private void Update()
    {
        if (_gameConfig.State != GameStates.Preparation)
        {
            if (_interactableHold != null)
            {
                Destroy(_interactableHold);
            }

            return;
        }

        CheckInventorySlotPressed(KeyCode.Alpha1, 0);
        CheckInventorySlotPressed(KeyCode.Alpha2, 1);
        CheckInventorySlotPressed(KeyCode.Alpha3, 2);
        CheckInventorySlotPressed(KeyCode.Alpha4, 3);
        CheckInventorySlotPressed(KeyCode.Alpha5, 4);
        CheckInventorySlotPressed(KeyCode.Alpha6, 5);
        CheckInventorySlotPressed(KeyCode.Alpha7, 6);
        CheckInventorySlotPressed(KeyCode.Alpha8, 7);
        CheckInventorySlotPressed(KeyCode.Alpha9, 8);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceItem();
        }
    }

    private void CheckInventorySlotPressed(KeyCode key, int index)
    {
        if (Input.GetKeyDown(key))
        {
            _playerData.Instance.ItemIndexSelected = index;

            if (_playerData.Instance.ItemSelected != null && _lastIndexSelected != index)
            {
                PreviewItem();
            }
            
            _lastIndexSelected = index;
        }
    }

    private void PreviewItem()
    {
        if (_interactableHold != null)
        {
            Destroy(_interactableHold);
        }

        var drop = _dropList.Interactables.FirstOrDefault(item => item.Type == _playerData.Instance.ItemSelected.Value.Type);
        
        if (drop.Prefab == null) return;

        var obj = Instantiate(drop.Prefab, transform);
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localPosition = new Vector3(0, obj.transform.localPosition.y, _offset);
        _interactableHold = obj;

        _interactableMeshRenderer = obj.GetComponent<MeshRenderer>();
        _collider = obj.GetComponent<Collider>();
        _wasTrigger = _collider.isTrigger;
        _collider.isTrigger = true;
        _previousMaterial = _interactableMeshRenderer.material;
        _interactableMeshRenderer.material = _previewMaterial;
    }

    private void PlaceItem()
    {
        if (_playerData.Instance.ItemSelected == null) return;
        
        _interactableMeshRenderer.material = _previousMaterial;
        _interactableHold.transform.parent = null;

        _interactableHold.GetComponent<InteractableController>().Setup(_playerData.Instance.ItemSelected.Value);
        _collider.isTrigger = _wasTrigger;
        
        _playerData.Instance.PropsAvailable[_playerData.Instance.ItemIndexSelected] = null;
        _interactableHold = null;
        _interactableMeshRenderer = null;
        _previousMaterial = null;
    }
}
