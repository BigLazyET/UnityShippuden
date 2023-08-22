using Assets.Scripts.Combat;
using Assets.Scripts.Common;

namespace Assets.Scripts.Weapons
{
    public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
    {
        private ActionHitBox hitBox;
        private CoreSystem.Movement movement;

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            movement = weapon.Core.GetCoreComponent<CoreSystem.Movement>();
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        private void HandleDetectedCollider2D(UnityEngine.Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if(collider.gameObject.TryGetComponentInChildren(out IKnockBackable knockBackable))
                {
                    knockBackable.KnockBack(new Combat.KnockBackData(currentAttackData.Angle, movement.FacingDirection, currentAttackData.Strength, weapon.Core.Root));
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}
