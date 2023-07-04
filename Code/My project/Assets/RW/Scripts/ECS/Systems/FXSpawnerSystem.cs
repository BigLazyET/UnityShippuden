using Unity.Burst;
using Unity.Entities;

public partial struct FXSpawnerSystem : ISystem
{
    private Entity explosionEntityFromPrefab;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // first way
        state.EntityManager.CompleteDependencyBeforeRO<FXComponent>();  // TODO
        explosionEntityFromPrefab = SystemAPI.GetSingleton<FXComponent>().Value;

        // another way
        var queryBuilder = new EntityQueryBuilder(state.WorldUpdateAllocator).WithAll<FXComponent>();   // TODO
        var query = queryBuilder.Build(ref state);
        explosionEntityFromPrefab = query.GetSingleton<FXComponent>().Value;
        explosionEntityFromPrefab = SystemAPI.QueryBuilder().WithAll<FXComponent>().Build().GetSingleton<FXComponent>().Value;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new SpawnDestructionFXJob
        {
            ecbWriter = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            prefabEntity = explosionEntityFromPrefab
        };

        job.Schedule();
    }
}
