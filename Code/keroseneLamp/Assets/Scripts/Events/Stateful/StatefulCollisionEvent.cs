using Unity.Assertions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Events
{
    public struct StatefulCollisionEvent : IBufferElementData, IStatefulSimulationEvent<StatefulCollisionEvent>
    {
        public Entity EntityA { get; set; }

        public Entity EntityB { get; set; }

        public int BodyIndexA { get; set; }

        public int BodyIndexB { get; set; }

        public ColliderKey ColliderKeyA { get; set; }

        public ColliderKey ColliderKeyB { get; set; }

        public StatefulEventState State { get; set; }

        // EntityA point to EntityB
        public float3 Normal;

        // Only if CalculateDetails is checked on StatefulCollisionEventDetailsSwitch of selected entity,
        // this field will have valid value, otherwise it will be zero initialized
        internal Details CollisionDetails;

        public StatefulCollisionEvent(CollisionEvent collisionEvent)
        {
            EntityA = collisionEvent.EntityA;
            EntityB = collisionEvent.EntityB;
            BodyIndexA = collisionEvent.BodyIndexA;
            BodyIndexB = collisionEvent.BodyIndexB;
            ColliderKeyA = collisionEvent.ColliderKeyA;
            ColliderKeyB = collisionEvent.ColliderKeyB;
            State = default;
            Normal = collisionEvent.Normal;
            CollisionDetails = default;
        }

        // Returns the other entity in EntityPair, if provided with other one
        public Entity GetOtherEntity(Entity entity)
        {
            Assert.IsTrue((entity == EntityA) || (entity == EntityB));
            return entity == EntityA ? EntityB : EntityA;
        }

        // Returns the normal pointing from passed entity to the other one in pair
        public float3 GetNormalFrom(Entity entity)
        {
            Assert.IsTrue((entity == EntityA) || (entity == EntityB));
            return math.select(-Normal, Normal, entity == EntityB);
        }

        public bool TryGetDetails(out Details details)
        {
            details = CollisionDetails;
            return CollisionDetails.IsValid;
        }

        public int CompareTo(StatefulCollisionEvent other)
        {
            return ISimulationEventUtilities.CompareEvents(this, other);
        }
    }
}
