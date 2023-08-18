using Assets.Scripts.CoreSystem;
using Assets.Scripts.ProjectileSystem;
using System;

namespace Assets.Scripts.Weapons
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        public event Action OnSpawnProjectile;

        private Movement movement;
        private IProjectileSpawnerStrategy strategy;


        protected override void HandleOnExit()
        {
            base.HandleOnExit();


        }

        private void HandleAttackAction()
        {
            foreach (var spawnInfo in currentAttackData.SpawnInfos)
            {
                //strategy.ExecuteSpawnerStrategy(spawnInfo, transform.position,movement.FacingDirection,)
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

            movement = Core.GetCoreComponent<Movement>();
            AnimationEventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnAttackAction -= HandleAttackAction;
        }
        #endregion
    }
}
