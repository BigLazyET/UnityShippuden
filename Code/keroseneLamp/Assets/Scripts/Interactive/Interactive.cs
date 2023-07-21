using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public abstract class Interactive : MonoBehaviour, IInteractive
    {
        public GameObject[] triggers;   // 互动触发能与之接触的物体集合

        // 针对普通互动和特殊互动
        protected bool isTouch;   // 两者是否在互动触发的接触范围内
        public bool isNeedTouch;   // 互动触发是否需要接触
        protected bool isInput;   // 互动触发的前提：按键是否按下且正确按键
        public bool isNeedInput;   // 互动触发是否需要按键输入

        // 针对特殊互动：此情况比较特殊，可能后期获得了某个装备/到达了某个地方/解锁了某个技能都能触发特殊互动
        private bool isSpecialCheck;  // 特殊互动的先决条件

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var trigger = collision.gameObject;
            isTouch = triggers.Contains(trigger);
        }

        public abstract void NormalActive();

        public abstract void SpecificActive();
    }
}
