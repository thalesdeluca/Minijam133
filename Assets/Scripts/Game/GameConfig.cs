﻿
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Config/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public float InvincibilityTime;
    public float HitStopTime;
    public float FireDamage;

    public GameStates State;
    public Transform Player;

    public UnityEvent OnStateChanged;
}


public enum GameStates
{
    MainMenu,
    Preparation,
    Combat,
    Fuse,
    Death
}