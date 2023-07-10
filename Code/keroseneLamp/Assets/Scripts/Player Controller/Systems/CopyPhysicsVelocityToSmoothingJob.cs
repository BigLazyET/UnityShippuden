using Unity.Burst;
using Unity.Entities;
using Unity.Physics.GraphicsIntegration;

namespace PlayerController
{
    /// <summary>
    /// // override the behavior of CopyPhysicsVelocityToSmoothing
    /// </summary>
    [BurstCompile]
    public partial struct CopyPhysicsVelocityToSmoothingJob : IJobEntity
    {
        public void Execute(in PlayerControllerInternal playerControllerInternal,
            ref PhysicsGraphicalSmoothing smoothing) 
        {
            smoothing.CurrentVelocity = playerControllerInternal.Velocity;
        }
    }
}
