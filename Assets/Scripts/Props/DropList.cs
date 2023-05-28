
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DropList", menuName = "ScriptableObjects/Drops/DropList")]
public class DropList : ScriptableObject
{
    public List<Drop> Drops;
    public List<Interactable> Interactables;
    public DropController DropPrefab;

    public Drop GetProp(DropType type)
    {
        return Drops.FirstOrDefault(e => e.Type == type);
    }
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

