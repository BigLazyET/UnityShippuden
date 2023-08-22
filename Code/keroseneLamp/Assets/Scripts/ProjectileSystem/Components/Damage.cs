using Assets.Scripts.Combat;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class Damage : ProjectileComponent
    {
        private HitBox hitBox;
        private float amount;
        private float lastDamageTime;

        [field: SerializeField] public LayerMask layerMask { get; private set; }
        [field: SerializeField] public bool SetInactiveAfterDamage { get; private set; }
        [field: SerializeField] public float CoolDown { get; private set; }

        public event Action<IDamageable> OnDamage;
        public event Action<RaycastHit2D> OnRaycastHit;

        protected override void HandleInit()
        {
            base.HandleInit();

            lastDamageTime = Mathf.NegativeInfinity;
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

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DamageDataPackage damageData) return;

            amount = damageData.Amount;
        }

        private void HandleRaycastHit(RaycastHit2D[] hits)
        {
            if (!Active) return;

            if (Time.time < lastDamageTime + CoolDown) return;

            foreach (var hit in hits)
            {
                if (!hit.collider.gameObject.IsLayerInMask(layerMask)) continue;

                if (!hit.collider.transform.gameObject.TryGetComponentInChildren(out IDamageable component))
                    continue;
                component.Damage(new DamageData(amount, projectile.gameObject));

                OnDamage?.Invoke(component);
                OnRaycastHit?.Invoke(hit);

                lastDamageTime = Time.time;

                if (SetInactiveAfterDamage)
                    SetActive(false);

                return;
            }
        }
    }
}
