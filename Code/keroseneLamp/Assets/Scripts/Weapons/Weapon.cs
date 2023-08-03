using Assets.Scripts.Core;
using Assets.Scripts.SO;
using System;
using UnityEngine;
using CoreNs = Assets.Scripts.Core;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// This script attach to PrimaryWeapon/SecondaryWeapon GameObject directly
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        // set in inspector
        [SerializeField] private float attackCounterResetCoolDown;

        // events
        public event Action<bool> OnCurrentInputChange;

        // fields
        private bool initDone;
        private bool currentInput;
        private int currentAttackCounter;

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

        public float AttackStartTime { get; private set; }
        public CoreNs.Core Core { get; private set; }
        public WeaponDataSO WeaponData { get; private set; }
        public Animator animator { get; private set; }
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public void SetCore(CoreNs.Core core) => Core = core;
    }
}
