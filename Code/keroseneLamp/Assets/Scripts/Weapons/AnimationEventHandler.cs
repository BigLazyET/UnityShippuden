using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 定义在 Animation 上的 Trigger Event
    /// 需要在Editor Animation面板上创建 Trigger Event，并绑定方法
    /// </summary>
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnAttackAction;

        private void AttackActionTrigger() => OnAttackAction?.Invoke();
    }
}
