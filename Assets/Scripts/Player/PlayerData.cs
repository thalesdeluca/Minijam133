using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [HideInInspector] private static PlayerData _instance;

    [HideInInspector]
    public PlayerData Instance
    {
        get => _instance;
    }

    [Header("Player")]
    public float Health;
    public float Speed;

    [Header("Inventory")] 
    public int MaxInventorySize;
    
    [Header("Projectile")]
    public float Attack;
    public float BulletSpeed;
    public float ReloadSpeed;
    
    [Header("Ammo")]
    public int MagazineAmmo;
    public int CurrentAmmo;
    public int TotalAmmo;
    
    [Header("Weapon Powerups")]
    public bool HasSlow;
    public bool CanIgnite;
    public bool CanSmoke;
    
    [Header("Runtime")]
    [HideInInspector] public Vector3 AimPosition;

    public List<InteractableProperties?> PropsAvailable = new List<InteractableProperties?>();
    public Dictionary<DropType, int> Drops = new Dictionary<DropType, int>();

    public bool CanCreateNewItems => PropsAvailable.Count < MaxInventorySize;
    public int ItemIndexSelected;
    public bool IsReloading;
    public float InvincibilityTime;

    public InteractableProperties? ItemSelected =>
        PropsAvailable.Count > ItemIndexSelected ? PropsAvailable[ItemIndexSelected] : null;

    public void CreateInstance()
    {
        _instance = ScriptableObject.CreateInstance<PlayerData>();
        _instance.Health = Health;
        _instance.Speed = Speed;
        
        _instance.Attack = Attack;
        _instance.BulletSpeed = BulletSpeed;
        _instance.ReloadSpeed = ReloadSpeed;
        _instance.MagazineAmmo = MagazineAmmo;
        _instance.CurrentAmmo = CurrentAmmo;
        _instance.TotalAmmo = TotalAmmo;
        
        _instance.HasSlow = HasSlow;
        _instance.CanIgnite = CanIgnite;
        _instance.CanSmoke = CanSmoke;
        
        _instance.MaxInventorySize = MaxInventorySize;
        _instance.PropsAvailable = new List<InteractableProperties?>(PropsAvailable.ToArray());
        _instance.PropsAvailable.Add(new InteractableProperties()
        {
            Type = DropType.Shield,
            Health = 4,
            IsObstacle = true,
            Level = 1
        });
        
        _instance.PropsAvailable.Add(new InteractableProperties()
        {
            Type = DropType.Shield,
            Health = 4,
            IsObstacle = true,
            Level = 1
        });
        
        _instance.PropsAvailable.Add(new InteractableProperties()
        {
            Type = DropType.Fire,
            Health = 0,
            IsFireTrap = true,
            Level = 1
        });

        
        _instance.Drops = new Dictionary<DropType, int>(Drops);

        // _instance.PropsAvailable.Clear();
        // _instance.Drops.Clear();
    }
}