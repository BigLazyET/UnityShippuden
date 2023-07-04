using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct SpawnDestructionFXJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecbWriter;
    public Entity prefabEntity;

    public void Execute([ChunkIndexInQuery] int chunkIndex, in LocalTransform transform, in Entity entity, in DestroyedTag destroyed)
    {
        var newEntity = ecbWriter.Instantiate(chunkIndex, prefabEntity);
        ecbWriter.SetComponent(chunkIndex, newEntity, new LocalTransform { Position = transform.Position });
        ecbWriter.RemoveComponent(chunkIndex, entity, typeof(DestroyedTag));
        ecbWriter.AddComponent(chunkIndex, entity, typeof(CleanupTag));
    }
}

