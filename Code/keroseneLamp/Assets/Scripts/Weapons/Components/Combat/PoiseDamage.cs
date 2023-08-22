using Assets.Scripts.Combat;
using Assets.Scripts.Common;

namespace Assets.Scripts.Weapons
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox hitBox;

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += HandleDetectedCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectedCollider2D -= HandleDetectedCollider2D;
        }

        private void HandleDetectedCollider2D(UnityEngine.Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if(collider.gameObject.TryGetComponentInChildren(out IPoisonable poisonable))
                {
                    poisonable.Poison(new PoisonData(currentAttackData.Amount, weapon.Core.Root));
                }
            }
        }
    }
}
