using Assets.Scripts.Combat;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class PoiseDamage : ProjectileComponent
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        private HitBox hitBox;
        private float amount;

        public event Action<IPoisonable> OnPoiseDamage;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not PoiseDamageDataPackage poiseDamageDataPackage)
                return;

            amount = poiseDamageDataPackage.Amount;
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
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask)) continue;

                if (!hit.collider.transform.gameObject.TryGetComponentInChildren(out IPoisonable component)) continue;

                component.Poison(new PoisonData(amount, projectile.gameObject));

                OnPoiseDamage?.Invoke(component);

                return;
            }
        }
    }
}
