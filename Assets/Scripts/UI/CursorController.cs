
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CursorController : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        Cursor.visible = false;
    }

    private void Update()
    {
        _image.transform.position = Input.mousePosition;
    }
}
