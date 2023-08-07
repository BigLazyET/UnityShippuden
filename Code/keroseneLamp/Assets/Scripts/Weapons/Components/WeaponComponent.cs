using Assets.Scripts.CoreSystem;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected bool isAttackActive;

        protected Core Core => weapon.Core;

        protected float AttackStartTime => weapon.AttackStartTime;

        protected AnimationEventHandler AnimationEventHandler => weapon.EventHandler;

        private void Awake()
        {
            weapon = GetComponent<Weapon>();
        }
    }
}
