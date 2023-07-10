using Unity.Mathematics;
using Unity.Physics;

namespace Events
{
    public struct StepInput
    {
        public PhysicsWorldSingleton PhysicsWorldSingleton;
        public float DeltaTime;
        public float3 Gravity;
        public float3 Up;
        public int MaxIterations;
        public float Tau;
        public float Damping;
        public float SkinWidth;
        public float ContactTolerance;
        public float MaxSlope;
        public int RigidBodyIndex;
        public float3 CurrentVelocity;
        public float MaxMovementSpeed;
    }
}
