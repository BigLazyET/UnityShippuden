using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;

// [BurstCompile]   // 不再需要把BurstCompile加到ISystem的实现struct上
// 但是其Create()，Update()等方法必须加上
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CleanupSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // eiecbHandle = state.World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        // 直接获取EndInitializationEntityCommandBufferSystem引用将导致当前System无法被brust compiler
        //eiecbRef = state.World.GetOrCreateSystemManaged<EndInitializationEntityCommandBufferSystem>();

        state.RequireForUpdate<CleanupTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // state.EntityManager.GetComponentData<FooComponent>(eiecbHandle);
        var ecb = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        // 已经在OnCreate中指定了Require CleanupTag，所以这边GetAllEntities的前提是具备CleanupTag Component，TODO：待考证
        var entities = state.EntityManager.GetAllEntities(Allocator.TempJob);
        
        var query = SystemAPI.QueryBuilder().WithAll<RefRO<CleanupTag>>().Build();
        //ecb.DestroyEntity(query, EntityQueryCaptureMode.AtPlayback);    // AtRecord：Immediately，AtPlayback：Deferred but great performance improvement

        entities = query.ToEntityArray(Allocator.TempJob);
        foreach (var entity in entities) 
        {
            ecb.DestroyEntity(entity);
        }

        foreach (var (cleanup,entity) in SystemAPI.Query<RefRO<CleanupTag>>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
    }
}

//public struct FooComponent : IComponentData
//{
//    public ushort handle;
//}
