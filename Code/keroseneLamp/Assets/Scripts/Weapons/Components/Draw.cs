using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 该组件负责在释放输入时评估动画曲线(Animation Curve)，然后广播该值
    /// </summary>
    public class Draw : WeaponComponent<DrawData, AttackDraw>
    {
        public event Action<float> OnEvaluateCurve;

        private bool hasEvaluatedDraw;
        private float drawPercentage;

        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasEvaluatedDraw) return;

            hasEvaluatedDraw = true;
            drawPercentage = currentAttackData.DrawCurve
                .Evaluate(Mathf.Clamp((Time.time - weapon.AttackStartTime) / currentAttackData.DrawTime, 0f, 1f));
            OnEvaluateCurve?.Invoke(drawPercentage);
        }

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            hasEvaluatedDraw = false;
        }

        #region Lifecycle
        protected override void Awake()
        {
            base.Awake();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion
    }
}
