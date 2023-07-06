using Unity.Assertions;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace PlayerController
{
    [BurstCompile]
    public partial struct PlayerControllerJob : IJobChunk
    {
        public float deltaTime;

        [ReadOnly] public PhysicsWorldSingleton PhysicsWorldSingleton;
        [ReadOnly] public ComponentTypeHandle<PlayerController> playerControllerHandle; // readonly - 标记PlayerController本身是Player的基础配置信息，一般来说不可更改

        public ComponentTypeHandle<LocalTransform> localTransformHandle;

        // Stores impulses we wish to apply to dynamic bodies the character is interacting with.
        // This is needed to avoid race conditions when 2 characters are interacting with the
        // same body at the same time.
        [NativeDisableParallelForRestriction] public NativeStream.Writer DeferredImpulseWriter;

        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            Assert.IsFalse(useEnabledMask);

            var chunkPlayerControllerData = chunk.GetNativeArray(ref playerControllerHandle);
            var chunkLocalTransformData = chunk.GetNativeArray(ref localTransformHandle);
        }
    }
}
