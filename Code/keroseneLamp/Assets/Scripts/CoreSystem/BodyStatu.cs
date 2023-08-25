using System;
using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    [Serializable]
    public class BodyStatu
    {
        [field: SerializeField] public float MaxValue { get; private set; }

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
