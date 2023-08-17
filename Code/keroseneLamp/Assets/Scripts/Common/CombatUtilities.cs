using Assets.Scripts.Combat;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class CombatUtilities
    {
        public static void Damage(GameObject gameObject, DamageData damageData)
        {
            if (gameObject.TryGetComponentInChildren(out IDamageable component))
            {
                component.Damage(damageData);
            }
        }

        public static void KnockBack(GameObject gameObject, KnockBackData knockBackData)
        {
            if (gameObject.TryGetComponentInChildren(out IKnockBackable component))
            {
                component.KnockBack(knockBackData);
            }
        }

        public static void PoiseDamage(GameObject gameObject, PoisonData poisonData)
        {
            if (gameObject.TryGetComponentInChildren(out IPoisonable component))
            {
                component.Poison(poisonData);
            }
        }

        public static void Damage(Collider2D[] colliders, DamageData damageData)
        {
            foreach (var collider in colliders)
            {
                Damage(collider.gameObject, damageData);
            }
        }

        public static void KnockBack(Collider2D[] colliders, KnockBackData knockBackData)
        {
            foreach (var collider in colliders)
            {
                KnockBack(collider.gameObject, knockBackData);
            }
        }

        public static void PoiseDamage(Collider2D[] colliders, PoisonData poisonData)
        {
            foreach (var collider in colliders)
            {
                PoiseDamage(collider.gameObject, poisonData);
            }
        }
    }
}
