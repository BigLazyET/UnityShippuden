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
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        public event Action OnMinHoldPassed;

        public event Action OnConsumeInput;
        public event Action OnEnableInterrupt;
        public event Action<bool> OnSetOptionalSpriteActive;
        public event Action<AttackPhases> OnEnterAttackPhases;  // 针对不同阶段的weapon aprite切换

        public event Action<AnimationWindow> OnStartAnimationWindow;
        public event Action<AnimationWindow> OnStopAnimationWindow;

        private void FinishTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();


        private void ConsumeInputTrigger() => OnConsumeInput?.Invoke();
        private void EnableInterruptTrigger() => OnEnableInterrupt?.Invoke();
        private void SetOptionalSpriteEnableTrigger() => OnSetOptionalSpriteActive?.Invoke(true);
        private void SetOptionalSpriteActiveDisable() => OnSetOptionalSpriteActive?.Invoke(false);
        private void EnterAttackPhasesTrigger(AttackPhases phases) => OnEnterAttackPhases?.Invoke(phases);


        private void StartAnimationWindowTrigger(AnimationWindow window) => OnStartAnimationWindow?.Invoke(window);
        private void StopAnimationWindowTrigger(AnimationWindow window) => OnStopAnimationWindow?.Invoke(window);
    }
}
