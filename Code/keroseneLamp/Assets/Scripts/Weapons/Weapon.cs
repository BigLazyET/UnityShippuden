using Assets.Scripts.Common;
using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using System;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// This script attach to PrimaryWeapon/SecondaryWeapon GameObject directly
    /// Weapon 是跟 PlayerAttackState 相互联系的：Player 进入到 Attack State，然后再由Weapon接管进行一系列的操作
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        // set in inspector
        [SerializeField] private float attackCounterResetCoolDown;

        // events
        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnConsumeInput;
        public event Action<bool> OnCurrentInputChange;

        // fields
        private bool initDone;
        private bool currentInput;  // bound to player attack state
        private int currentAttackCounter;
        private AnimationEventHandler eventHandler;
        private TimeNotifier attackCounterResetTimer;  // while time has passed reset time, then Reset attack counter to 0

        // properties
        public bool CurrentInput
        {
            get => currentInput;
            set
            {
                if(currentInput != value)
                {
                    currentInput = value;
                    OnCurrentInputChange?.Invoke(currentInput);
                }
            }
        }
        
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= WeaponData.NumberOfAttacks ? 0 : value;
        }

        public AnimationEventHandler EventHandler
        {
            get => eventHandler;
            private set => eventHandler = value;
        }

        public float AttackStartTime { get; private set; }
        public Core Core { get; private set; }
        public WeaponDataSO WeaponData { get; private set; }
        public Animator Animator { get; private set; }  // 也是作为了WeaponDataSO的一项资源，其本质上被WeaponDataSO.AnimatorController赋值
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public void SetCore(Core core) => Core = core;

        public void SetData(WeaponDataSO weaponData) => WeaponData = weaponData;

        #region Bound to Player Attack State
        public void Enter()
        {
            AttackStartTime = Time.time;

            Animator.SetBool("active", true);
            Animator.SetInteger("counter", currentAttackCounter);

            attackCounterResetTimer.Disable();

            OnEnter?.Invoke();
        }

        public void Exit()
        {
            Animator.SetBool("active", false);

            CurrentAttackCounter++;
            attackCounterResetTimer.Init(attackCounterResetCoolDown);

            OnExit?.Invoke();
        }
        #endregion

        #region Lifecycle
        private void Awake()
        {
            GetDependencies();

            attackCounterResetTimer = new TimeNotifier();
        }

        private void OnEnable()
        {
            EventHandler.OnConsumeInput += HandleConsumeInput;
            attackCounterResetTimer.OnNotify += ResetAttackCounter;
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void OnDisable()
        {
            EventHandler.OnConsumeInput -= HandleConsumeInput;
            attackCounterResetTimer.OnNotify -= ResetAttackCounter;
        }
        #endregion

        #region Private Funcs
        private void ResetAttackCounter()
        {
            CurrentAttackCounter = 0;
        }

        private void GetDependencies()
        {
            if(initDone) return;

            BaseGameObject = transform.Find("Base").gameObject;
            Animator = BaseGameObject.GetComponent<Animator>();
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            initDone = true;
        }

        private void HandleConsumeInput() => OnConsumeInput?.Invoke();
        #endregion
    }
}
