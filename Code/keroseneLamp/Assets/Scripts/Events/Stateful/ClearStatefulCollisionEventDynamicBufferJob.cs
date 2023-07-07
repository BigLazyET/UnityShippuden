using Unity.Burst;
using Unity.Entities;

namespace Events
{
    [BurstCompile]
    public partial struct ClearStatefulCollisionEventDynamicBufferJob : IJobEntity
    {
        public void Execute(ref DynamicBuffer<StatefulCollisionEvent> eventBuffer) => eventBuffer.Clear();
    }
}
