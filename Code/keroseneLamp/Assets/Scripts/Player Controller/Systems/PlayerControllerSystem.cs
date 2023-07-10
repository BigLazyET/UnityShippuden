using Events;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.GraphicsIntegration;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace PlayerController
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    public partial struct PlayerControllerSystem : ISystem
    {
        EntityQuery playerControllerQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            playerControllerQuery = SystemAPI.QueryBuilder()
                .WithAllRW<LocalTransform>()
                .WithAllRW<PlayerController, PlayerControllerInternal>()
                .WithAll<PhysicsCollider>().Build();

            state.RequireForUpdate(playerControllerQuery);
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var chunks = playerControllerQuery.ToArchetypeChunkArray(Allocator.TempJob);
            var deferredImpluses = new NativeStream(chunks.Length, Allocator.TempJob);
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            var dt = SystemAPI.Time.DeltaTime;
            var playContollerJob = new PlayerControllerJob
            {
                playerControllerHandle = SystemAPI.GetComponentTypeHandle<PlayerController>(true),
                playerControllerInternalHandle = SystemAPI.GetComponentTypeHandle<PlayerControllerInternal>(),
                physicsColliderHandle = SystemAPI.GetComponentTypeHandle<PhysicsCollider>(true),
                localTransformHandle = SystemAPI.GetComponentTypeHandle<LocalTransform>(),
                statefulCollisionEventHandle = SystemAPI.GetBufferTypeHandle<StatefulCollisionEvent>(),
                statefuleTriggerEventHandle = SystemAPI.GetBufferTypeHandle<StatefulTriggerEvent>(),

                deltaTime = dt,
                PhysicsWorldSingleton = physicsWorldSingleton,
                DeferredImpulseWriter = deferredImpluses.AsWriter()
            };

            state.Dependency = playContollerJob.ScheduleParallel(playerControllerQuery, state.Dependency);

            var smoothedPlayerControllQuery = SystemAPI.QueryBuilder().WithAll<PlayerControllerInternal, PhysicsGraphicalSmoothing>().Build();
            var copyVelocityHandle = new CopyPhysicsVelocityToSmoothingJob().ScheduleParallel(smoothedPlayerControllQuery, state.Dependency);

            var applyJob = new ApplyDefferedPhysicsUpdatesJob
            {
                Chunks = chunks,
                DeferredImpulseReader = deferredImpluses.AsReader(),
                PhysicsVelocityLookup = SystemAPI.GetComponentLookup<PhysicsVelocity>(),
                PhysicsMassLookup = SystemAPI.GetComponentLookup<PhysicsMass>(true),
                LocalTransformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true),
            };

            state.Dependency = applyJob.Schedule(state.Dependency);
            state.Dependency = deferredImpluses.Dispose(state.Dependency);
            state.Dependency = JobHandle.CombineDependencies(state.Dependency, copyVelocityHandle);
        }
    }
}
