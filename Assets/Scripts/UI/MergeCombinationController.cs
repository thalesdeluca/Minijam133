
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeCombinationController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private DropList _dropList;
    
    [Header("Drop1")]
    [SerializeField] private Image _dropImage1;
    [SerializeField] private TMP_Text _dropText1;
    
    [SerializeField] private Image _glueImage;
    [SerializeField] private TMP_Text _glueText;

    [Header("Drop2")]
    [SerializeField] private Image _dropImage2;
    [SerializeField] private TMP_Text _dropText2;

    [Header("Result")]
    [SerializeField] private Image _resultImage;

    [SerializeField] private TMP_Text _amountResult;

    [SerializeField] private TMP_Dropdown _levelDropdown;
    [SerializeField] private Button _fuseButton;
    
    private Combination _combination;

    public void Setup(Combination combination)
    {
        _combination = combination;
        var drop1 = _dropList.GetProp(combination.Drop1);
        var drop2 = _dropList.GetProp(combination.Drop2);

        _dropImage1.sprite = drop1.Sprite;
        _dropImage2.sprite = drop2.Sprite;
        _resultImage.sprite = _dropList.GetInteractable(combination.Resulting).Icon;

        _fuseButton.onClick.AddListener(OnFuseClick);
    }

    private void Update()
    {
        var isDrop1Unavailable = !_playerData.Instance.Drops.TryGetValue(_combination.Drop1, out var drop1Amount) ||
                                 drop1Amount <= 0 || drop1Amount < _levelDropdown.value + 1;
        
        var isDrop2Unavailable = !_playerData.Instance.Drops.TryGetValue(_combination.Drop2, out var drop2Amount) ||
                                 drop2Amount <= 0 || drop2Amount < _levelDropdown.value + 1;
        
        var isGlueUnavailable = !_playerData.Instance.Drops.TryGetValue(DropType.Ammo, out var glueAmount) ||
                                glueAmount <= 0 || glueAmount < _combination.GlueAmount * _levelDropdown.value + 1;
        
        _fuseButton.interactable = !(isDrop1Unavailable || isDrop2Unavailable || isGlueUnavailable || !_playerData.Instance.CanCreateNewItems);

        var value = _levelDropdown.value + 1;
        _dropText1.text = $"x{value}";
        _dropText2.text = $"x{value}";
        _glueText.text = $"x{_combination.GlueAmount * value}";
        _amountResult.text = _combination.ResultAmount > 1 ? $"x{_combination.ResultAmount * value}" : "";
    }

    private void OnFuseClick()
    {
        var level = _levelDropdown.value + 1;
        
        _playerData.Instance.Drops[_combination.Drop1] -= level;
        _playerData.Instance.Drops[_combination.Drop2] -= level;
        _playerData.Instance.Drops[DropType.Ammo] -= (int)_combination.GlueAmount * level;
        
        if (_combination.Resulting == DropType.Ammo)
        {
            _playerData.Instance.TotalAmmo += _combination.ResultAmount * level;
            return;
        }
        
        if (_combination.Resulting == DropType.AttackUp)
        {
            _playerData.Instance.Attack += level;
            return;
        }
            
        var interactable = _dropList.GetInteractable(_combination.Resulting);
       
        var availableSlot = _playerData.Instance.PropsAvailable.IndexOf(null);

        _playerData.Instance.PropsAvailable.Insert(availableSlot, new InteractableProperties()
        {
            Type = _combination.Resulting,
            IsObstacle = _combination.Resulting == DropType.Shield,
            IsFireTrap = _combination.Resulting == DropType.Fire,
            IsSlowTrap = _combination.Resulting == DropType.Water,
            Damage = interactable.Damage * level,
            Health = interactable.Health * level,
            Level = level
        });
    }
}
