﻿using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class BodyStatus: CoreComponent
    {
        // set in inspector
        [SerializeField] public BodyStatu Health { get; private set; }  // 理解：玩家的生命初始值
        [SerializeField] public BodyStatu Poison { get; private set; }  // 理解：玩家的耐毒初始值，当耐毒值归零后，玩家处于晕厥状态
        [SerializeField] public float PoisonRecoveryRate { get; private set; }

        public override void Init()
        {
            base.Init();

            Health.Init();
            Poison.Init();
        }

        private void Update()
        {
            if (Poison.CurrentValue.Equals(Poison.MaxValue)) return;

            Poison.Increase(PoisonRecoveryRate * Time.deltaTime);   // TODO?
        }
    }

    [Serializable]
    public class BodyStatu
    {
        // set in inspector
        [SerializeField] public float MaxValue { get; private set; }

        // properties
        private float currentValue;
        public event Action OnCurrentValueZero;

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);
                if(currentValue < 0f )
                    OnCurrentValueZero.Invoke();
            }
        }

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount) => CurrentValue -= amount;
    }
}
