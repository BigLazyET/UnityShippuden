using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        // 存在多个Projectile的情况
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfo { get;private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo
    {
        [field: SerializeField] public Vector2 Offset { get; private set; }

        [field: SerializeField] public Vector2 Direction { get; private set; }

        [field: SerializeField] public GameObject Projectile { get; private set; }
    }
}
