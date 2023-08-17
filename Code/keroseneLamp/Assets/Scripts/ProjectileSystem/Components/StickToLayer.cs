using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class StickToLayer : ProjectileComponent
    {
        public event Action setStuck;
        public event Action setUnStuck;

        [field: SerializeField] public string InactiveSortingLayerName { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private HitBox hitBox;
        private bool isStuck;
        private float gravityScale;
        private SpriteRenderer spriteRenderer;
        private string activeSortingLayerName;

        private Transform newReferenceTransform;
        private bool subscribedToDisableNotifier;
        private OnDisableNotifier onDisableNotifier;

        private Vector3 offsetPosition;
        private Quaternion offsetRotation;

        protected override void HandleReset()
        {
            base.HandleReset();

            SetUnStuck();
        }

        protected override void Awake()
        {
            base.Awake();

            gravityScale = projectile.Rigidbody2D.gravityScale;

            hitBox = GetComponent<HitBox>();
            hitBox.OnRaycastHit += HandleRaycastHit;

            spriteRenderer = GetComponent<SpriteRenderer>();
            activeSortingLayerName = spriteRenderer.sortingLayerName;
        }

        protected override void Update()
        {
            base.Update();

            if (!isStuck) return;

            if (!newReferenceTransform)
            {
                SetUnStuck();
                return;
            }

            // only stuck, we need update
            transform.position = newReferenceTransform.position + newReferenceTransform.rotation * offsetPosition;  // TODO?
            transform.rotation = newReferenceTransform.rotation * offsetRotation;   // TODO?
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit -= HandleRaycastHit;

            if (subscribedToDisableNotifier)
                onDisableNotifier.OnDisabled -= HandleOnDisableNotifier;
        }

        private void HandleRaycastHit(UnityEngine.RaycastHit2D[] hits)
        {
            if (!Active) return;

            SetStuck();

            var lineHit = Physics2D.Raycast(transform.position, transform.right, CheckDistance, LayerMask);

            if (lineHit)
            {
                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) continue;

                SetReferenceTransformAndPoint(hit.transform, hit.point);
                return;
            }

            SetUnStuck();
        }

        private void SetStuck()
        {
            isStuck = true;

            projectile.Rigidbody2D.bodyType = RigidbodyType2D.Static;
            projectile.Rigidbody2D.velocity = Vector2.zero;
            spriteRenderer.sortingLayerName = InactiveSortingLayerName;

            setStuck?.Invoke();
        }

        private void SetUnStuck()
        {
            isStuck = false;

            projectile.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            projectile.Rigidbody2D.gravityScale = gravityScale;
            spriteRenderer.sortingLayerName = activeSortingLayerName;

            setUnStuck?.Invoke();
        }

        private void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint)
        {
            if (newReferenceTransform.TryGetComponent(out onDisableNotifier))
            {
                onDisableNotifier.OnDisabled += HandleOnDisableNotifier;
                subscribedToDisableNotifier = true;
            }

            transform.position = newPoint;

            this.newReferenceTransform = newReferenceTransform;
            offsetPosition = transform.position - newReferenceTransform.position;
            offsetRotation = Quaternion.Inverse(newReferenceTransform.rotation) * transform.rotation;
        }

        private void HandleOnDisableNotifier()
        {
            SetUnStuck();

            if (!subscribedToDisableNotifier) return;

            onDisableNotifier.OnDisabled -= HandleOnDisableNotifier;
            subscribedToDisableNotifier = false;
        }
    }
}
