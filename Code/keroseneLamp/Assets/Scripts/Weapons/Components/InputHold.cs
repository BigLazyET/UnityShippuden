using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class InputHold : WeaponComponent
    {
        private bool input;
        private bool minHoldPassed;
        private Animator animator;

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            minHoldPassed = false;
        }

        private void HandleMinHoldPassed()
        {
            minHoldPassed = false;
            SetAnimatorParameter();
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            input = newInput;
            SetAnimatorParameter();
        }

        private void SetAnimatorParameter()
        {
            if(input)
                animator.SetBool("hold", input);

            if (minHoldPassed)
                animator.SetBool("hold", false);
        }

        #region Lifecycle
        protected override void Awake()
        {
            base.Awake();

            animator = GetComponentInChildren<Animator>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
            weapon.AnimationEventHandler.OnMinHoldPassed += HandleMinHoldPassed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
            weapon.AnimationEventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
        }
        #endregion
    }
}
