using System;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _aimPlaneDistance;
     private Vector3 _direction;
    private Ray _ray;
    private Plane _plane;

    private void Awake()
    {
        _plane = new Plane(transform.up, _aimPlaneDistance);
    }

    void Update()
    { 
        var mousePos = Input.mousePosition;

        var viewportPoint = Camera.main.ScreenToViewportPoint(mousePos);
        
        if (viewportPoint.y > 1 || viewportPoint.y < 0) return;
       
        if (viewportPoint.x > 1 || viewportPoint.x < 0) return;
       
        _ray = Camera.main.ScreenPointToRay(mousePos);
        var worldPos = _ray.GetPoint(Vector3.Distance(_ray.origin, transform.position));
        if (_plane.Raycast(_ray, out var distance))
        {
            worldPos = _ray.GetPoint(distance);
        }

        worldPos.y = transform.position.y;
        transform.LookAt(worldPos);

        _playerData.Instance.AimPosition = worldPos;
    }
    
}
