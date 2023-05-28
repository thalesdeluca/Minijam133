using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float Health;
    public float Speed;
    public float Damage;

    public bool CanSlow;
    public bool CanIgnite;

    public List<DropType> Drops;

    public EnemyData CreateInstance()
    {
        var data = ScriptableObject.CreateInstance<EnemyData>();

        data.Health = Health;
        data.Speed = Speed;
        data.Damage = Damage;
        data.CanIgnite = CanIgnite;
        data.CanSlow = CanSlow;
        data.Drops = new List<DropType>(Drops.ToArray());
        return data;
    }
}
