using Unity.Entities;

namespace Events
{
    public struct StatefulCollisionEventDetailsSwitch : IComponentData
    {
        public bool isCalculateDetails;
    }
}
