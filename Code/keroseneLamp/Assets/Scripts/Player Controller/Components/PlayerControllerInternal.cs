using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.GraphicsIntegration;

namespace PlayerController
{
    /// <summary>
    /// 实际运动中，Player参与移动/跳跃/看向等的信息
    /// </summary>
    [WriteGroup(typeof(PhysicsGraphicalInterpolationBuffer))]
    [WriteGroup(typeof(PhysicsGraphicalSmoothing))]
    public struct PlayerControllerInternal : IComponentData
    {
        public float CurrentRotationAngle;
        public PlayerSupportState SupportedState;
        public float3 UnsupportedVelocity;
        public PhysicsVelocity Velocity;
        public Entity Entity;
        public bool IsJumping;
        public Input Input;
    }
}
