using Assets.Scripts.ProjectileSystem;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Weapons
{
    public class ProjectileSpawnerStrategy : IProjectileSpawnerStrategy
    {
        private Vector2 spawnPosition;
        private Vector2 spawnDirection;
        private Projectile currentProjectile;

        public virtual void ExecuteSpawnerStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, 
                                                   int facingDirection, ObjectPool<Projectile> objectPool, Action<Projectile> OnSpawnProjectile)
        {
            SpawnProjectile(projectileSpawnInfo,projectileSpawnInfo.Direction,spawnerPos, facingDirection, objectPool, OnSpawnProjectile);
        }

        protected virtual void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Vector2 spawnDirection,
            Vector3 spawnPos, int facingDirection, ObjectPool<Projectile> objectPool, Action<Projectile> OnSpawnProjectile)
        {
            SetSpawnDirection(spawnDirection, facingDirection);
            SetSpawnPosition(spawnPos, projectileSpawnInfo.Offset, facingDirection);
            InitializeProjectile(projectileSpawnInfo, objectPool, OnSpawnProjectile);
        }

        protected virtual void SetSpawnDirection(Vector2 direction, int facingDriection)
        {
            spawnDirection.Set(direction.x * facingDriection, direction.y);
        }

        protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDriection)
        {
            spawnPosition.Set(referencePosition.x + offset.x * facingDriection, referencePosition.y + offset.y);
        }

        protected virtual void InitializeProjectile(ProjectileSpawnInfo projectileSpawnInfo, ObjectPool<Projectile> objectPool, Action<Projectile> OnSpawnProjectile)
        {
            currentProjectile = objectPool.Get();

            currentProjectile.transform.position = spawnPosition;
            var angle = Mathf.Atan2(spawnDirection.y, spawnDirection.x) * Mathf.Rad2Deg;
            currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            currentProjectile.Reset();
            currentProjectile.SendDataPackage(projectileSpawnInfo.DamageDataPackage);
            currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackDataPackage);
            currentProjectile.SendDataPackage(projectileSpawnInfo.PoiseDamageDataPackage);
            currentProjectile.SendDataPackage(projectileSpawnInfo.SpriteDataPackage);

            OnSpawnProjectile?.Invoke(currentProjectile);

            currentProjectile.Init();
        }
    }
}
