using Assets.Scripts.CoreSystem;
using Assets.Scripts.ObjectPoolSystem;
using Assets.Scripts.ProjectileSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        public event Action<Projectile> OnSpawnProjectile;

        private CoreSystem.Movement movement;
        private IProjectileSpawnerStrategy strategy;

        private ObjectPool<Projectile> objectPool = ObjectPool.Create<Projectile>();

        protected override void HandleOnExit()
        {
            base.HandleOnExit();

            SetProjectileSpawnerStrategy(new ProjectileSpawnerStrategy());
        }

        private void HandleAttackAction()
        {
            foreach (var spawnInfo in currentAttackData.SpawnInfos)
            {
                strategy.ExecuteSpawnerStrategy(spawnInfo, transform.position, movement.FacingDirection, objectPool, OnSpawnProjectile);
            }
        }

        public void SetProjectileSpawnerStrategy(IProjectileSpawnerStrategy strategy)
        {
            this.strategy = strategy;
        }

        #region Lifecyle
        protected override void Awake()
        {
            base.Awake();

            SetProjectileSpawnerStrategy(new ProjectileSpawnerStrategy());
        }

        protected override void Start()
        {
            base.Start();

            movement = weapon.Core.GetCoreComponent<CoreSystem.Movement>();
            weapon.AnimationEventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.AnimationEventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if(data == null || !Application.isPlaying) return;

            foreach (var item in data.GetAllAttackData())
            {
                foreach (var spawnInfo in item.SpawnInfos)
                {
                    var position = transform.position + (Vector3)spawnInfo.Offset;

                    Gizmos.DrawWireSphere(position, 0.2f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(position, position + (Vector3)spawnInfo.Direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }
        #endregion
    }
}
