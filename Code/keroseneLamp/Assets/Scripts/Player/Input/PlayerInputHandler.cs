using Assets.Scripts.Player.Enums;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour, InputActions.IPlayerActions
    {
        private Camera camera;
        private PlayerInput playerInput;

        private float jumpInputStartTime;
        private float dashInputStartTime;
        [SerializeField] private float inputHoldTime = 0.2f;

        public int NormInputX { get; private set; }

        public int NormInputY { get; private set; }

        public bool JumpInput { get; private set; }

        public bool JumpInputStop { get; private set; }

        public bool GrapInput { get; private set; }

        public bool DashInput { get; private set; }

        public bool DashInputStop { get; private set; }

        public Vector2 RawMovementInput { get; private set; }

        public Vector2 RawDashDirectionInput { get; private set; }

        public Vector2Int DashDirectionInput { get; private set; }

        public bool[] AttackInputs { get; private set; }

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            AttackInputs = new bool[Enum.GetValues(typeof(CombatInputs)).Length];

            camera = Camera.main;
        }

        private void Update()
        {
            CheckJumpInputHoldTime();
            CheckDashIputHoldTime();
        }

        public void SilentJumpInputByInUse() => JumpInput = false;

        public void SilentDashInputByInUse() => DashInput = false;

        public void SilentAttackInput(int i) => AttackInputs[i] = false;

        public void CheckJumpInputHoldTime() => JumpInput = Time.time < jumpInputStartTime + inputHoldTime;

        public void CheckDashIputHoldTime() => DashInput = Time.time < dashInputStartTime + inputHoldTime;

        #region InputActions
        public void OnMove(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

            if (context.canceled)
                JumpInputStop = true;
        }

        public void OnGrap(InputAction.CallbackContext context)
        {
            if(context.started)
                GrapInput = true;

            if (context.canceled)
                GrapInput = false;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                DashInput = true; 
                DashInputStop = false;
                dashInputStartTime = Time.time;
            }

            if (context.canceled)
                DashInputStop = true;
        }

        public void OnDashDirection(InputAction.CallbackContext context)
        {
            RawDashDirectionInput = context.ReadValue<Vector2>();

            if (playerInput.currentControlScheme == "Mouse")
            {
                RawDashDirectionInput = camera.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position; 
            }

            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }

        public void OnPrimaryAttack(InputAction.CallbackContext context)
        {
            AttackInputs[((int)CombatInputs.Primary)] = context.started ? true : context.canceled ? false : AttackInputs[((int)CombatInputs.Primary)];
        }

        public void OnSecondaryAttack(InputAction.CallbackContext context)
        {
            AttackInputs[((int)CombatInputs.Secondary)] = context.started ? true : context.canceled ? false : AttackInputs[((int)CombatInputs.Secondary)];
        }
        #endregion
    }
}
