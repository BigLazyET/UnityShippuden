using Assets.Scripts.ObjectPoolSystem;
using Assets.Scripts.ProjectileSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 策略模式：https://refactoringguru.cn/design-patterns/strategy/csharp/example
    /// </summary>
    public interface IProjectileSpawnerStrategy
    {
        void ExecuteSpawnerStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection,
             ObjectPool<Projectile> objectPool, Action<Projectile> OnSpawnProjectile);
    }
}
