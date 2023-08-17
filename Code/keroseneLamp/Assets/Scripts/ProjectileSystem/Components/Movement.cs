using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class Movement : ProjectileComponent
    {
        [field: SerializeField] public bool ApplyContinuously { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        protected override void HandleInit()
        {
            base.HandleInit();

            SetVelocity();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!ApplyContinuously) return;

            SetVelocity();
        }

        private void SetVelocity() => projectile.Rigidbody2D.velocity = Speed * transform.right;
    }
}
