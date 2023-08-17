using Assets.Scripts.Common;

namespace Assets.Scripts.Weapons
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox hitbox;

        private void HandleDetectedCollider2D(UnityEngine.Collider2D[] colliders)
        {
            CombatUtilities.Damage(colliders, new Combat.DamageData(currentAttackData.Amount, Core.Root));
        }

        protected override void Start()
        {
            base.Start();

            hitbox = GetComponent<ActionHitBox>();
            hitbox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitbox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }
    }
}
