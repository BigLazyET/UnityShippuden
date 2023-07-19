using Unity.Entities;
using Unity.Physics;

namespace Events
{
    public interface IStatefulSimulationEvent<T> : IBufferElementData, ISimulationEvent<T>
    {
        public StatefulEventState State { get; set; }
    }
}
