
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropList", menuName = "ScriptableObjects/Drops/DropList")]
public class DropList : ScriptableObject
{
    public List<Drop> Drops;
    public List<Interactable> Interactables;
}

[Serializable]
public struct Drop
{
    public DropType Type;
    public Sprite Sprite;
}

[Serializable]
public struct Interactable
{
    public DropType Type;
    public GameObject Prefab;
}

public enum DropType
{
    Shield,
    Fire,
    Water,
    Ammo,
    AttackUp
}

