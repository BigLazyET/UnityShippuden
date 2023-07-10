using Unity.Assertions;
using Unity.Entities;
using Unity.Physics;

namespace Events
{
    public struct StatefulTriggerEvent : IBufferElementData, IStatefulSimulationEvent<StatefulTriggerEvent>
    {
        public Entity EntityA { get; set; }

        public Entity EntityB { get; set; }

        public int BodyIndexA { get; set; }

        public int BodyIndexB { get; set; }

        public ColliderKey ColliderKeyA { get; }

        public ColliderKey ColliderKeyB { get; }

        public StatefulEventState State { get; set; }

        public Entity GetOtherEntity(Entity entity)
        {
            Assert.IsTrue(EntityA == entity || entity == EntityB);
            return entity == EntityA ? EntityB : EntityA;
        }

        public int CompareTo(StatefulTriggerEvent other)
        {
            return ISimulationEventUtilities.CompareEvents(this, other);
        }
    }
}
