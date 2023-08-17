using Assets.Scripts.Combat;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class KnockBack : ProjectileComponent
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private HitBox hitBox;
        private float strength;
        private Vector2 angle;

        public event Action<IKnockBackable> OnKnockBack;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not KnockBackDataPackage knockBackDataPackage) return;

            strength = knockBackDataPackage.Strength;
            angle = knockBackDataPackage.Angle;
        }

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<HitBox>();
            hitBox.OnRaycastHit += HandleRaycastHit;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnRaycastHit -= HandleRaycastHit;
        }

        private void HandleRaycastHit(RaycastHit2D[] hits)
        {
            if (!Active) return;

            foreach (var hit in hits)
            {
                if (LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) continue;

                if (!hit.collider.transform.gameObject.TryGetComponentInChildren(out IKnockBackable component)) continue;

                component.KnockBack(new KnockBackData(angle, (int)Mathf.Sign(transform.right.x), strength, projectile.gameObject));

                OnKnockBack?.Invoke(component);

                return;
            }
        }
    }
}
