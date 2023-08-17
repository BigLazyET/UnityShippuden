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

        public virtual void Init() { }

        #region Lifecycle
        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void Start()
        {
            weapon.OnEnter += HandleOnEnter;
            weapon.OnExit += HandleOnExit;
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandleOnEnter;
            weapon.OnExit -= HandleOnExit;
        }
        #endregion

        #region Protected Funcs
        protected virtual void HandleOnEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleOnExit()
        {
            isAttackActive = false;
        }
        #endregion
    }

    public class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            currentAttackData = data.GetAttackData(weapon.CurrentAttackCounter);
        }

        public override void Init()
        {
            base.Init();

            data = weapon.WeaponData.GetComponentData<T1>();
        }
    }
}
