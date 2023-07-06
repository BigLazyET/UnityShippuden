using System;
using Unity.Mathematics;
using UnityEngine;

namespace PlayerController
{
    [Serializable]
    public class PlayerControllerAuthoring : MonoBehaviour
    {
        // Gravity force applied to the player controller body
        public float3 Gravity = Unity.Physics.PhysicsStep.Default.Gravity;

        // Speed of movement initiated by user input
        public float MovementSpeed = 2.5f;

        // Maximum speed of movement at any given time
        public float MaxMovementSpeed = 10.0f;

        // Speed of rotation initiated by user input
        public float RotationSpeed = 2.5f;

        // Speed of upwards jump initiated by user input
        public float JumpUpwardsSpeed = 5.0f;

        // Maximum slope angle player can overcome (in degrees)
        public float MaxSlope = 60.0f;

        // Maximum number of player controller solver iterations
        public int MaxIterations = 10;

        // Mass of the player (used for affecting other rigid bodies)
        public float PlayerMass = 1.0f;

        // Keep the player at this distance to planes (used for numerical stability)
        public float SkinWidth = 0.02f;

        // Anything in this distance to the player will be considered a potential contact
        // when checking support
        public float ContactTolerance = 0.1f;

        // Whether to affect other rigid bodies
        public bool AffectsPhysicsBodies = true;

        // Whether to raise collision events
        // Note: collision events raised by player controller will always have details calculated
        public bool RaiseCollisionEvents = false;

        // Whether to raise trigger events
        public bool RaiseTriggerEvents = false;
    }
}
