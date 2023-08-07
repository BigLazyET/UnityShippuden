using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using System;
using System.Timers;
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
        private Timer attackCounterResetTimer;  // while time has passed reset time, then Reset attack counter to 0

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
        public Animator animator { get; private set; }  // 也是作为了WeaponDataSO的一项资源，其本质上被WeaponDataSO.AnimatorController赋值
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public void SetCore(Core core) => Core = core;

        public void SetData(WeaponDataSO weaponData) => WeaponData = weaponData;

        #region Bound to Player Attack State
        public void Enter()
        {
            AttackStartTime = Time.time;
            attackCounterResetTimer.Enabled = false;

            animator.SetBool("active", true);
            animator.SetInteger("counter", currentAttackCounter);
        }

        public void Exit()
        {

        }
        #endregion
    }
}
