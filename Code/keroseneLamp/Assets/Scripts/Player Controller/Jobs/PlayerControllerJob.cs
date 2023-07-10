using Events;
using Unity.Assertions;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace PlayerController
{
    [BurstCompile]
    public partial struct PlayerControllerJob : IJobChunk
    {
        const float k_DefaultTau = 0.4f;
        const float k_DefaultDamping = 0.9f;

        public float deltaTime;

        [ReadOnly] public PhysicsWorldSingleton PhysicsWorldSingleton;
        [ReadOnly] public ComponentTypeHandle<PlayerController> playerControllerHandle; // readonly - 标记PlayerController本身是Player的基础配置信息，一般来说不可更改
        [ReadOnly] public ComponentTypeHandle<PhysicsCollider> physicsColliderHandle;

        public ComponentTypeHandle<PlayerControllerInternal> playerControllerInternalHandle;
        public ComponentTypeHandle<LocalTransform> localTransformHandle;
        public BufferTypeHandle<StatefulCollisionEvent> statefulCollisionEventHandle;
        public BufferTypeHandle<StatefulTriggerEvent> statefuleTriggerEventHandle;

        // Stores impulses we wish to apply to dynamic bodies the character is interacting with.
        // This is needed to avoid race conditions when 2 characters are interacting with the
        // same body at the same time.
        [NativeDisableParallelForRestriction] public NativeStream.Writer DeferredImpulseWriter;

        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            Assert.IsFalse(useEnabledMask);

            var chunkPlayerControllerData = chunk.GetNativeArray(ref playerControllerHandle);
            var chunkPlayerControllerInternalData = chunk.GetNativeArray(ref playerControllerInternalHandle);
            var chunkLocalTransformData = chunk.GetNativeArray(ref localTransformHandle);
            var chunkPhysicsColliderData = chunk.GetNativeArray(ref physicsColliderHandle);
            var hasChunkCollisionEventBufferType = chunk.Has(ref statefulCollisionEventHandle);
            var hasChunkTriggerEventBufferType = chunk.Has(ref statefuleTriggerEventHandle);

            BufferAccessor<StatefulCollisionEvent> collisionEventBuffers = default;
            BufferAccessor<StatefulTriggerEvent> triggerEventBuffers = default;
            if (hasChunkCollisionEventBufferType)
                collisionEventBuffers = chunk.GetBufferAccessor(ref statefulCollisionEventHandle);
            if (hasChunkTriggerEventBufferType)
                triggerEventBuffers = chunk.GetBufferAccessor(ref statefuleTriggerEventHandle);

            DeferredImpulseWriter.BeginForEachIndex(unfilteredChunkIndex);

            for (var i = 0; i < chunk.Count; ++i)
            {
                var playerController = chunkPlayerControllerData[i];
                var playerControllerInternal = chunkPlayerControllerInternalData[i];
                var localTransform = chunkLocalTransformData[i];
                var physicsCollider = chunkPhysicsColliderData[i];

                DynamicBuffer<StatefulCollisionEvent> collisionEventBuffer = default;
                if (hasChunkCollisionEventBufferType)
                    collisionEventBuffer = collisionEventBuffers[i];
                DynamicBuffer<StatefulTriggerEvent> triggerEventBuffer = default;
                if (hasChunkTriggerEventBufferType)
                    triggerEventBuffer = triggerEventBuffers[i];

                // Collision filter must be valid
                if (!physicsCollider.IsValid || physicsCollider.Value.Value.GetCollisionFilter().IsEmpty)
                    continue;

                var up = math.select(math.up(), -math.normalize(playerController.Gravity),
                        math.lengthsq(playerController.Gravity) > 0f);

                // Player step input
                StepInput stepInput = new StepInput
                {
                    PhysicsWorldSingleton = PhysicsWorldSingleton,
                    DeltaTime = deltaTime,
                    Up = up,
                    Tau = k_DefaultTau,
                    Damping = k_DefaultDamping,

                    Gravity = playerController.Gravity,
                    MaxIterations = playerController.MaxIterations,
                    SkinWidth = playerController.SkinWidth,
                    ContactTolerance = playerController.ContactTolerance,
                    MaxSlope = playerController.MaxSlope,
                    MaxMovementSpeed = playerController.MaxMovementSpeed,

                    RigidBodyIndex = PhysicsWorldSingleton.PhysicsWorld.GetRigidBodyIndex(playerControllerInternal.Entity),
                    CurrentVelocity = playerControllerInternal.Velocity.Linear
                };

                // Player transform
                RigidTransform transform = new RigidTransform
                {
                    pos = localTransform.Position,
                    rot = localTransform.Rotation,
                };

                NativeList<StatefulCollisionEvent> currentFrameCollisionEvents = 
                    playerController.RaiseCollisionEvents != 0 ? new NativeList<StatefulCollisionEvent>(Allocator.Temp) : default;
                NativeList<StatefulTriggerEvent> currentFrameTriggerEvents = 
                    playerController.RaiseTriggerEvents != 0 ? new NativeList<StatefulTriggerEvent> : default;
            }

            DeferredImpulseWriter.EndForEachIndex();
        }
    }
}
