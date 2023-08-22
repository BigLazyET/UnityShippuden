using Assets.Scripts.ProjectileSystem;

namespace Assets.Scripts.Weapons
{
    public class DrawToProjectile : WeaponComponent
    {
        private Draw draw;
        private ProjectileSpawner projectileSpawner;

        private readonly DrawModifierDataPackage drawModifierDataPackage = new DrawModifierDataPackage();

        protected override void HandleOnEnter()
        {
            drawModifierDataPackage.DrawPercentage = 0f;
        }

        private void HandleSpawnProjectile(ProjectileSystem.Projectile projectile)
        {
            projectile.SendDataPackage(drawModifierDataPackage);
        }

        private void HandleEvaluateCurve(float value)
        {
            drawModifierDataPackage.DrawPercentage = value;
        }

        #region Lifecycle
        protected override void Start()
        {
            base.Start();

            draw = GetComponent<Draw>();
            projectileSpawner = GetComponent<ProjectileSpawner>();

            draw.OnEvaluateCurve += HandleEvaluateCurve;
            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion
    }
}
