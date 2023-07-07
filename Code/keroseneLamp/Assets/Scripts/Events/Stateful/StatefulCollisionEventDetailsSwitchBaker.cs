using Events;
using Unity.Entities;

namespace Assets.Scripts.Events.Stateful
{
    public class StatefulCollisionEventDetailsSwitchBaker : Baker<StatefulCollisionEventDetailsSwitchAuthoring>
    {
        public override void Bake(StatefulCollisionEventDetailsSwitchAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            if (authoring.CalculateDetails)
                AddComponent(entity, new StatefulCollisionEventDetailsSwitch { isCalculateDetails = authoring.CalculateDetails });
            AddBuffer<StatefulCollisionEvent>(entity);
        }
    }
}
