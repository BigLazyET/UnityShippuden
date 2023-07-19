using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Events
{
    /// <summary>
    /// 
    /// </summary>
    public partial struct ConvertEventStreamToDynamicBufferJob<T, C> : IJob 
        where T : unmanaged, IBufferElementData, IStatefulSimulationEvent<T> 
        where C: unmanaged, IComponentData
    {
        public bool UseExcludeComponent;
        public NativeList<T> PreviousEvents;
        public NativeList<T> CurrentEvents;
        [ReadOnly] public ComponentLookup<C> EventExcludeLookup;

        // 这是我们将收集到的T需要添加到Entity的Lookup，因为T是IBufferElementData类型，所以意味着一个Entity可以多个T
        public BufferLookup<T> EventLookup; 

        public void Execute()
        {
            var statefulEvents = new NativeList<T>(Allocator.Temp);
            StatefulSimulationEventBuffers<T>.GetStatefulEvents(PreviousEvents, CurrentEvents, statefulEvents);

            for (int i = 0; i < statefulEvents.Length; i++)
            {
                var statefulEvent = statefulEvents[i];

                var addToEntityA = EventLookup.HasBuffer(statefulEvent.EntityA) &&
                    (!UseExcludeComponent || !EventExcludeLookup.HasComponent(statefulEvent.EntityA));
                var addToEntityB = EventLookup.HasBuffer(statefulEvent.EntityB) &&
                    (!UseExcludeComponent || !EventExcludeLookup.HasComponent(statefulEvent.EntityA));

                if (addToEntityA)
                    EventLookup[statefulEvent.EntityA].Add(statefulEvent);
                if (addToEntityB)
                    EventLookup[statefulEvent.EntityB].Add(statefulEvent);
            }
        }
    }
}
