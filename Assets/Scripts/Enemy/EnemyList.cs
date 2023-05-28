
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/EnemyList", fileName = "EnemyList")]
public class EnemyList: ScriptableObject
{
    public List<EnemyController> Enemies;
}

