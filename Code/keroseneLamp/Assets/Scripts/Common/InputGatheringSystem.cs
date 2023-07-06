using System;
using System.Numerics;
using Unity.Entities;
using UnityEngine.InputSystem;

namespace Common
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    partial class InputGatheringSystem : SystemBase, InputActions.IPlayerActions  // ISystem
    {
        InputActions inputActions;

        EntityQuery playerControllerInputQuery;

        Vector2 playerMovement;
        Vector2 playerLooking;
        float playerFiring;
        bool playerJumped;

        protected override void OnCreate()
        {
            inputActions = new InputActions();
            inputActions.Player.SetCallbacks(this);

            playerControllerInputQuery = GetEntityQuery(typeof(PlayerController.Input));
        }

        protected override void OnUpdate()
        {
            if (playerControllerInputQuery.CalculateEntityCount() == 0)
                EntityManager.CreateEntity(typeof(PlayerController.Input));

            playerControllerInputQuery.SetSingleton(new PlayerController.Input
            {
                looking = playerLooking,
                movement = playerMovement,
                jumped = playerJumped ? 1 : 0
            });

            playerJumped = false;
        }

        #region Player Actions
        void InputActions.IPlayerActions.OnFire(InputAction.CallbackContext context) => playerFiring = context.ReadValue<float>();

        void InputActions.IPlayerActions.OnJump(InputAction.CallbackContext context) => playerJumped = context.started;

        void InputActions.IPlayerActions.OnLook(InputAction.CallbackContext context) => playerLooking = context.ReadValue<Vector2>();

        void InputActions.IPlayerActions.OnMove(InputAction.CallbackContext context) => playerMovement = context.ReadValue<Vector2>();
        #endregion
    }
}
