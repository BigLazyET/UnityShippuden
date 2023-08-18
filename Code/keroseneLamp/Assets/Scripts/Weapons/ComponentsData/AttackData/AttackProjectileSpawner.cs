using Assets.Scripts.ProjectileSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        // 存在多个Projectile的情况
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get;private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo
    {
        [field: SerializeField] public Vector2 Offset { get; private set; }

        [field: SerializeField] public Vector2 Direction { get; private set; }

        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }

        // 生成Projectile需要weapon传递的data package
        [field: SerializeField] public DamageDataPackage DamageDataPackage { get; private set; }
        
        [field: SerializeField] public KnockBackDataPackage KnockBackDataPackage { get; private set; }
        
        [field: SerializeField] public PoiseDamageDataPackage PoiseDamageDataPackage { get; private set; }

        [field: SerializeField] public SpriteDataPackage SpriteDataPackage { get; private set; }
    }
}
