using Unity.Burst;
using Unity.Entities;

public partial struct DamageSystem : ISystem
{
    private float impactDistance;

    [BurstCompile]
    public void Update(ref SystemState state)
    {
        // https://forum.unity.com/threads/how-do-you-get-an-entitycommandbuffersystem-from-an-isystem-struct.1404325/
        // You do not need AddHandleForProducer when doing it this way any more. Dependency is managed by the singleton
        var ecbWriter = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

        var job = new CheckForCollisionsJob
        {
            quadrantMultHahsMap = QuadrantSystem.quadrantMultiHashMap,
            //playerPostition =     // TODO
            collisionDistance = impactDistance,
            ecbWriter = ecbWriter
        };
        job.ScheduleParallel();
    }
}
