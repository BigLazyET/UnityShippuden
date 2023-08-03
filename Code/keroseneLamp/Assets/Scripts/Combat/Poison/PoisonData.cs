using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class PoisonData
    {
        /// <summary>
        /// 中毒值
        /// </summary>
        public float Amount { get; private set; }

        /// <summary>
        /// 承受者
        /// </summary>
        public GameObject Taker { get; private set; }

        public PoisonData(float amount, GameObject taker)
        {
            this.Amount = amount;
            this.Taker = taker;
        }

        public void SetAmount(float amount) => Amount = amount;
    }
}
