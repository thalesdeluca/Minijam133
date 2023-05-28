
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

    public List<Combination> Combinations;

    public Drop GetProp(DropType type)
    {
        return Drops.FirstOrDefault(e => e.Type == type);
    }
    
    public Interactable GetInteractable(DropType type)
    {
        return Interactables.FirstOrDefault(e => e.Type == type);
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
    public Sprite Icon;
    public GameObject Prefab;
    public float Damage;
    public float Health;
}

public enum DropType
{
    Shield,
    Fire,
    Water,
    Ammo,
    AttackUp
}

[Serializable]
public struct Combination
{
    public DropType Drop1;
    public DropType Drop2;
    public float GlueAmount;

    public DropType Resulting;
    public int ResultAmount;
}

