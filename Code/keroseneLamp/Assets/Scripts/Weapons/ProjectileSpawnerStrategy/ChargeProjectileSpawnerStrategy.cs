using Assets.Scripts.ObjectPoolSystem;
using Assets.Scripts.ProjectileSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        // 以下字段会被其他组件赋值
        public float AngleVaration; // 角度变化
        public float ChargeAmount;

        private Vector2 currentDirection;   // 下一个Projectile的Direction

        public override void ExecuteSpawnerStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection, ObjectPool<Projectile> objectPool, Action<Projectile> OnSpawnProjectile)
        {
            if (ChargeAmount == 0) return;

            if (ChargeAmount == 1)
                currentDirection = projectileSpawnInfo.Direction;
            else
            {
                var rotation = Quaternion.Euler(0, 9, -((ChargeAmount - 1) * AngleVaration / 2f));
                currentDirection = rotation * projectileSpawnInfo.Direction;

                for (int i = 0; i < ChargeAmount; i++)
                {
                    SpawnProjectile(projectileSpawnInfo, currentDirection, spawnerPos, facingDirection, objectPool, OnSpawnProjectile);

                    // Rotate the spawn direction for next projectile
                    currentDirection = Quaternion.Euler(0, 0, AngleVaration) * currentDirection;
                }
            }
        }
    }
}
