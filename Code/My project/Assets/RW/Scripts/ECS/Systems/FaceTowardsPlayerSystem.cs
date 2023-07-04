using Unity.Burst;
using Unity.Entities;

public partial struct FaceTowardsPlayerSystem : ISystem
{
    [BurstCompile]
    public void OnUpate(ref SystemState state)
    {
        // Entities.ForEach is deprecated in ISystem-based systems.
        // There are now two APIs you can use to iterate over entities: IJobEntity or QueryEnginee，but  i recommend IJobEntity
        var job = new FaceForwardPlayerJob
        {
            // playerPosition = // TODO
        };
        job.Schedule();
    }
}
