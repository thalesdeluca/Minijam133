
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform _muzzle;

    private bool _isReloading;
    private void Update()
    {
        if (_gameConfig.State != GameStates.Combat)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetButton("Reload") && !_isReloading)
        {
            StartCoroutine(WaitReloadTime());
        }
    }

    IEnumerator WaitReloadTime()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_playerData.Instance.ReloadSpeed);
        Reload();
        _isReloading = false;
    }

    private void Reload()
    {
        if (_playerData.Instance.TotalAmmo <= 0 || _playerData.Instance.CurrentAmmo >= _playerData.Instance.MagazineAmmo) return;

        var ammoAvailable = Mathf.Min(_playerData.Instance.MagazineAmmo, _playerData.Instance.TotalAmmo);

        if (ammoAvailable <= 0)
        {
            // add no ammo sound
            return;
        }

        _playerData.Instance.TotalAmmo -= ammoAvailable;
        
        var finalAmmo = Mathf.Clamp(ammoAvailable + _playerData.Instance.CurrentAmmo, 0, _playerData.Instance.MagazineAmmo);

        if (finalAmmo == 0) return;
        
        _playerData.Instance.CurrentAmmo = finalAmmo;
        // run reload animations
    }

    private void Shoot()
    {
        if (_playerData.Instance.CurrentAmmo > 0)
        {
            _playerData.Instance.CurrentAmmo--;
            ProjectilePool.Instance.SpawnProjectile(new ProjectileProperties()
            {
                Damage = _playerData.Instance.Attack,
                Direction = _playerData.Instance.AimPosition - transform.position,
                BulletSpeed = _playerData.Instance.BulletSpeed,
                Position = _muzzle.position,
                HasSlow = _playerData.Instance.HasSlow,
                CanIgnite = _playerData.Instance.CanIgnite,
                CanSmoke = _playerData.Instance.CanSmoke
            });
            // play shoot sound and animations
        }
    }
}
