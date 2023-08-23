using Assets.Scripts.Combat;
using Assets.Scripts.Common;

namespace Assets.Scripts.Weapons
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox hitbox;

        private void HandleDetectedCollider2D(UnityEngine.Collider2D[] colliders)
        {
            foreach (var collider in colliders) // 这边的collider是指武器造成了伤害的对象的碰撞器，比如敌人的碰撞器等
            {
                if(collider.gameObject.TryGetComponentInChildren(out IDamageable damageable))
                {
                    damageable.Damage(new Combat.DamageData(currentAttackData.Amount, weapon.Core.Root));
                }
            }
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
