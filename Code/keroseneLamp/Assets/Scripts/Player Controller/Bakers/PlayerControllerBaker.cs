using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace PlayerController
{
    public class PlayerControllerBaker : Baker<PlayerControllerAuthoring>
    {
        public override void Bake(PlayerControllerAuthoring authoring)
        {
            if (authoring != null && authoring.enabled && authoring.isActiveAndEnabled)
            {
                var playerController = new PlayerController
                {
                    Gravity = authoring.Gravity,
                    MovementSpeed = authoring.MovementSpeed,
                    MaxMovementSpeed = authoring.MaxMovementSpeed,
                    RotationSpeed = authoring.RotationSpeed,
                    JumpUpwardsSpeed = authoring.JumpUpwardsSpeed,
                    MaxSlope = math.radians(authoring.MaxSlope),
                    MaxIterations = authoring.MaxIterations,
                    PlayerMass = authoring.PlayerMass,
                    SkinWidth = authoring.SkinWidth,
                    ContactTolerance = authoring.ContactTolerance,
                    AffectsPhysicsBodies = (byte)(authoring.AffectsPhysicsBodies ? 1 : 0),
                    RaiseCollisionEvents = (byte)(authoring.RaiseCollisionEvents ? 1 : 0),
                    RaiseTriggerEvents = (byte)(authoring.RaiseTriggerEvents ? 1 : 0)
                };

                var playerControllerInternal = new PlayerControllerInternal
                {
                    Entity = GetEntity(TransformUsageFlags.Dynamic),
                    Input = new Input()
                };

                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, playerController);
                AddComponent(entity, playerControllerInternal);
            }
        }
    }
}
