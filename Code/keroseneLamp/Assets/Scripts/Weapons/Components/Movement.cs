using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.Movement coreMovement;

        private float velocity;
        private Vector2 direction;

        private void HandleStartMovement()
        {
            velocity = currentAttackData.Velocity;
            direction = currentAttackData.Direction;

            coreMovement.SetVelocity(velocity, direction, coreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            velocity = 0f;
            direction = Vector2.zero;

            coreMovement.SetVelocity(velocity, direction, coreMovement.FacingDirection);
        }

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            velocity = 0f;
            direction = Vector2.zero;
        }

        #region Lifecycle
        protected override void Start()
        {
            base.Start();

            coreMovement = weapon.Core.GetCoreComponent<CoreSystem.Movement>();

            weapon.AnimationEventHandler.OnStartMovement += HandleStartMovement;
            weapon.AnimationEventHandler.OnStopMovement += HandleStopMovement;
        }

        private void FixedUpdate()
        {
            if (!isAttackActive) return;

            coreMovement.SetVelocityX((velocity * direction).x * coreMovement.FacingDirection);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.AnimationEventHandler.OnStartMovement -= HandleStartMovement;
            weapon.AnimationEventHandler.OnStopMovement -= HandleStopMovement;
        }
        #endregion
    }
}
