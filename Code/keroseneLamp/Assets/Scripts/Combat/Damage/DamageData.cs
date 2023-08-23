using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class DamageData
    {
        /// <summary>
        /// 伤害值
        /// </summary>
        public float Amount { get; private set; }

        /// <summary>
        /// 攻击发起者 - 攻击来源 - 伤害源头
        /// </summary>
        public GameObject Source { get; private set; }

        public DamageData(float amount, GameObject source)
        {
            this.Amount = amount;
            this.Source = source;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}
