using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using static UnityEngine.EventSystems.EventTrigger;

[RequireMatchingQueriesForUpdate]
public partial struct LifetimeSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LifetimeComponent>();    // TODO：maybe equality to WithAll Attribute Or RequireMatchingQueriesForUpdate Attribute
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        //var query = SystemAPI.QueryBuilder().WithAll<RefRW<LifetimeComponent>>().Build();
        //using var entities = query.ToEntityArray(Allocator.TempJob);
        //// var lifetimes = query.ToComponentDataArray<LifetimeComponent>(Allocator.Temp);

        //foreach (var entity in entities)
        //{
        //    var lifetime = SystemAPI.GetComponent<LifetimeComponent>(entity);
        //    lifetime.timeAlive -= SystemAPI.Time.DeltaTime;
        //    if (lifetime.timeAlive <= 0)
        //        ecb.DestroyEntity(entity);
        //}

        foreach (var (lifetime,entity) in SystemAPI.Query<RefRW<LifetimeComponent>>().WithEntityAccess())
        {
            lifetime.ValueRW.timeAlive -= SystemAPI.Time.DeltaTime;
            if (lifetime.ValueRO.timeAlive <= 0)
                ecb.DestroyEntity(entity);
        }
    }
}
