using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ActionHitBox: WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;

        private Vector2 offset;
        private Collider2D[] detected;
        private CoreSystem.Movement movement;

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
            if (data == null) return;

            foreach (var attackData in data.GetAllAttackData())
            {
                if (!attackData.debug) continue;

                Gizmos.DrawWireCube(transform.position + (Vector3)attackData.HitBox.center, attackData.HitBox.size);
            }
        }

        private void HandleAttackAction()
        {
            offset.Set(transform.position.x + (currentAttackData.HitBox.center.x * movement.FacingDirection),
                       transform.position.y + (currentAttackData.HitBox.center.y));

            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayer);
            if (detected.Length == 0) return;

            OnDetectedCollider2D?.Invoke(detected);
        }
    }
}
