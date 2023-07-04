using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct QuadrantSystem : ISystem
{
    const int quadrantYMultiplier = 1000;
    const int quadrantCellSize = 10;
    static Plane plane = new Plane(Vector3.up, 0);

    public static NativeParallelMultiHashMap<int, QuadrantData> quadrantMultiHashMap;

    public static int GetPositionHashMapKey(float3 position)
    {
        return (int)(math.floor(position.x / quadrantCellSize) + quadrantYMultiplier * math.floor(position.z / quadrantCellSize));
    }

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        quadrantMultiHashMap = new NativeParallelMultiHashMap<int, QuadrantData>(0, Allocator.Persistent);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        quadrantMultiHashMap.Dispose();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var query = SystemAPI.QueryBuilder().WithAll<RefRO<LocalTransform>, RefRO<QuadrantTag>>().Build();

        if(query.CalculateEntityCount() > quadrantMultiHashMap.Capacity)
            quadrantMultiHashMap.Capacity = query.CalculateEntityCount();

        var job = new SetQuadrantDataHashMapJob
        {
            quadrantMultiHashMap = quadrantMultiHashMap,
        };

        job.Schedule();

        // for debug
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            // 沿着射线方向在【distance距离单位】的点
            var fieldPoint = ray.GetPoint(distance);
            DebugDrawQuadrant(fieldPoint);
            Debug.Log(quadrantMultiHashMap.CountValuesForKey(GetPositionHashMapKey(fieldPoint)) + " " + fieldPoint);
        }
    }

    static void DebugDrawQuadrant(float3 position)
    {
        var lowerLeft = new Vector3(math.floor(position.x / quadrantCellSize) * quadrantCellSize, 0, math.floor(position.z / quadrantCellSize) * quadrantCellSize);
        Debug.DrawLine(lowerLeft, lowerLeft + Vector3.right * quadrantCellSize);
        Debug.DrawLine(lowerLeft, lowerLeft + Vector3.forward * quadrantCellSize);
        Debug.DrawLine(lowerLeft + Vector3.right * quadrantCellSize, lowerLeft + new Vector3(1, 0, 1) * quadrantCellSize);
        Debug.DrawLine(lowerLeft + Vector3.forward * quadrantCellSize, lowerLeft + new Vector3(1, 0, 1) * quadrantCellSize);

        Debug.DrawLine(position, Camera.main.ScreenPointToRay(Input.mousePosition).origin);
    }
}

public static class NativeParallelMultiHashMapExtensions
{
    // TODO: 不需要扩展方法，直接CountValuesForKey即可
    public static int GetEntityCount(ref this NativeParallelMultiHashMap<int, QuadrantData> hashmap, int hashMapKey)
    {
        //int count = 0;
        //if (hashmap.TryGetFirstValue(hashMapKey, out QuadrantData quadrant, out NativeParallelMultiHashMapIterator<int> iterator))
        //{
        //    do { count++; } while (hashmap.TryGetNextValue(out quadrant, ref iterator));
        //}
        //return count;

        return hashmap.CountValuesForKey(hashMapKey);
    }
}
