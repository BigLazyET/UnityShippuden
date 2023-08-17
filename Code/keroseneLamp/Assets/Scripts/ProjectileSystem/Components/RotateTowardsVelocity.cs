using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class RotateTowardsVelocity : ProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var velocity = projectile.Rigidbody2D.velocity;

            if (velocity == Vector2.zero) return;

            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
