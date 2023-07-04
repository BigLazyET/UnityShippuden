using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct SetQuadrantDataHashMapJob : IJobEntity
{
    public NativeParallelMultiHashMap<int, QuadrantData> quadrantMultiHashMap;

    public void Execute([EntityIndexInQuery]int index, in LocalTransform transform, in Entity entity, in QuadrantTag tag, in MaxDistanceComponent maxDistance)
    {
        var hashMapKey = QuadrantSystem.GetPositionHashMapKey(transform.Position);
        quadrantMultiHashMap.Add(hashMapKey, new QuadrantData
        {
            entity = entity,
            position = transform.Position,
            quadrantTag = tag,
            allowedDistance = maxDistance.allowedDistance
        });
    }
}

public struct QuadrantData
{
    public Entity entity;
    public float3 position;
    public QuadrantTag quadrantTag;
    public float allowedDistance;
}
