using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[WithNone(typeof(CleanupTag), typeof(DestroyedTag))]
public partial struct CheckForCollisionsJob : IJobEntity
{
    public NativeParallelMultiHashMap<int, QuadrantData> quadrantMultHahsMap;
    public float3 playerPostition;
    public float collisionDistance;
    public EntityCommandBuffer.ParallelWriter ecbWriter;

    [BurstCompile]
    void Execute([ChunkIndexInQuery] int chunkIndex, in LocalTransform localTransform, in Entity entity, in QuadrantTag tag, in MaxDistanceComponent maxDistance)
    {
        var hashMapKey = QuadrantSystem.GetPositionHashMapKey(localTransform.Position);

        // entity: tag = Enemy
        if (tag.unitType == QuadrantUnitType.Enemy)
        {
            if (math.distance(localTransform.Position, playerPostition) <= collisionDistance)
            {
                ecbWriter.AddComponent(chunkIndex, entity, typeof(DestroyedTag));
            }
            else if (quadrantMultHahsMap.TryGetFirstValue(hashMapKey, out QuadrantData quadrant, out NativeParallelMultiHashMapIterator<int> iterator))
            {
                do
                {
                    // entity: tag = Enemy
                    // quadrant = bullet
                    if (quadrant.quadrantTag.unitType == QuadrantUnitType.Bullet)
                    {
                        if (math.distance(localTransform.Position, quadrant.position) <= collisionDistance)
                        {
                            // TODO: OnDamaged

                            // Add destroy tag to enemy
                            ecbWriter.AddComponent(chunkIndex, entity, typeof(DestroyedTag));
                            // do we need add destroy tag to bullet

                            break;
                        }
                    }
                } while (quadrantMultHahsMap.TryGetNextValue(out quadrant, ref iterator));
            }
        }
    }
}
