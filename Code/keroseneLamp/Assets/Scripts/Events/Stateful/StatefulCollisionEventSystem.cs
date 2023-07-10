using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Events
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    public partial struct StatefulCollisionEventSystem : ISystem
    {
        StatefulSimulationEventBuffers<StatefulCollisionEvent> events;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            events = new StatefulSimulationEventBuffers<StatefulCollisionEvent> ();
            events.AllocateBuffers();

            state.RequireForUpdate<StatefulCollisionEvent>();
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var clearJob = new ClearStatefulCollisionEventDynamicBufferJob();
            clearJob.ScheduleParallel();

            events.SwapBuffers();

            var previousEvents = events.Previous;
            var currentEvents = events.Current;

            // Collect Stateful Collision Events
            state.Dependency = new CollectCollisionEventsWithDetailsJob
            {
                CollisionEvents = currentEvents,    // 最终Collect的Events
                PhysicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld,
                EventDetailsSwitch = SystemAPI.GetComponentLookup<StatefulCollisionEventDetailsSwitch>(true),
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);

            // 必须依赖上一步的job结束之后才能进行，只有上一步job结束，Events才能有值
            // 为什么，它不是Struct值类型么；请记住，所有 NativeContainer 结构类型都存储指向其基础数据的指针（是的，C 样式指针）。这是因为一直复制内存是昂贵的
            // https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/components-nativecontainers.html
            // https://forum.unity.com/threads/why-are-structs-passed-by-reference-in-jobs.877204/
            state.Dependency = new ConvertEventStreamToDynamicBufferJob<StatefulCollisionEvent, DummyExcludeComponent>
            {
                PreviousEvents = previousEvents,
                CurrentEvents = currentEvents,
                EventLookup = SystemAPI.GetBufferLookup<StatefulCollisionEvent>(),

            }.Schedule(state.Dependency);
        }
    }
}
