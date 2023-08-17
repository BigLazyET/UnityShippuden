using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class HitBox : ProjectileComponent
    {
        [field: SerializeField] public Rect HitBoxRect { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        public event Action<RaycastHit2D[]> OnRaycastHit;

        private float checkDistance;

        private void CheckHitBox()
        {
            var hits = Physics2D.BoxCastAll(transform.TransformPoint(HitBoxRect.center), HitBoxRect.size,
                transform.rotation.eulerAngles.z, transform.right, checkDistance, LayerMask);   // TODO?

            if (hits == null || hits.Length == 0) return;

            OnRaycastHit?.Invoke(hits);
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            checkDistance = projectile.Rigidbody2D.velocity.magnitude * Time.deltaTime;

            CheckHitBox();
        }

        private void OnDrawGizmosSelected()
        {
            // The following is some code that ChatGPT Generated for me to visualize the HitBoxRect based on the rotation.
            // Set up gizmo color
            Gizmos.color = Color.red;

            // Create a new matrix that applies the projectile's rotation
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position,
                Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Vector3.one);
            Gizmos.matrix = rotationMatrix;

            // Draw the wireframe cube
            Gizmos.DrawWireCube(HitBoxRect.center, HitBoxRect.size);
        }
    }
}
