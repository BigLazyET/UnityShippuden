using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Physics;
using Unity.VisualScripting;

namespace Events
{
    public partial struct CollectCollisionEventsWithDetailsJob : ICollisionEventsJob
    {
        public bool IsForceCalculateDetails;
        public NativeList<StatefulCollisionEvent> CollisionEvents;

        [ReadOnly] public PhysicsWorld PhysicsWorld;
        [ReadOnly] public ComponentLookup<StatefulCollisionEventDetailsSwitch> EventDetailsSwitch;

        public void Execute(CollisionEvent collisionEvent)
        {
            var statefulCollisionEvent = new StatefulCollisionEvent(collisionEvent);

            var isCalculateDetails = IsForceCalculateDetails;
            if (!isCalculateDetails && EventDetailsSwitch.HasComponent(collisionEvent.EntityA))
                isCalculateDetails = EventDetailsSwitch[collisionEvent.EntityA].isCalculateDetails;
            if (!isCalculateDetails && EventDetailsSwitch.HasComponent(collisionEvent.EntityB))
                isCalculateDetails = EventDetailsSwitch[collisionEvent.EntityB].isCalculateDetails;

            if (isCalculateDetails)
            {
                var details = collisionEvent.CalculateDetails(ref PhysicsWorld);
                statefulCollisionEvent.CollisionDetails = new Details(
                    details.EstimatedContactPointPositions.Length,
                    details.EstimatedImpulse,
                    details.AverageContactPointPosition
                    );
            }

            CollisionEvents.Add(statefulCollisionEvent);
        }
    }
}
