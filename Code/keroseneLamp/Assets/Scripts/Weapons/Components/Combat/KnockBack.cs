using Assets.Scripts.Common;
using Assets.Scripts.CoreSystem;

namespace Assets.Scripts.Weapons
{
    public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
    {
        private ActionHitBox hitBox;
        private Movement movement;

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            movement = Core.GetCoreComponent<Movement>();
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        private void HandleDetectedCollider2D(UnityEngine.Collider2D[] colliders)
        {
            CombatUtilities.KnockBack(colliders, 
                new Combat.KnockBackData(currentAttackData.Angle,movement.FacingDirection, currentAttackData.Strength, Core.Root));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}
