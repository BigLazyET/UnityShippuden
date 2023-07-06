using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
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
            playerControllerQuery = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>().WithAllRW<PlayerController>().WithAll<PhysicsCollider>().Build();

            state.RequireForUpdate(playerControllerQuery);
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }
    }
}
