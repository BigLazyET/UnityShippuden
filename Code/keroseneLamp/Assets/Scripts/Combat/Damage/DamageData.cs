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
        /// 承受者
        /// </summary>
        public GameObject Taker { get; private set; }

        public DamageData(float amount, GameObject taker)
        {
            this.Amount = amount;
            this.Taker = taker;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}
