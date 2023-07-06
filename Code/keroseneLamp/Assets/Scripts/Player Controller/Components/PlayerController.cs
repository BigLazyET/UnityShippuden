using System;
using Unity.Entities;
using Unity.Mathematics;

namespace PlayerController
{
    /// <summary>
    /// Player本身的信息 - 类似配置信息
    /// </summary>
    [Serializable]
    public struct PlayerController : IComponentData
    {
        public float3 Gravity;
        public float MovementSpeed;
        public float MaxMovementSpeed;
        public float RotationSpeed;
        public float JumpUpwardsSpeed;
        public float MaxSlope; // radians
        public int MaxIterations;
        public float PlayerMass;
        public float SkinWidth;
        public float ContactTolerance;
        public byte AffectsPhysicsBodies;
        public byte RaiseCollisionEvents;
        public byte RaiseTriggerEvents;
    }
}
