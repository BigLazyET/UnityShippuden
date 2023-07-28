using Assets.Scripts.Enums;
using System.Collections.Concurrent;

namespace Assets.Scripts.Events.ObserveLikeModeUpgrade
{
    public class FooListener<T>
    {
        public static ConcurrentDictionary<EventSourceType, IFooEventSource<T>> sources = new ConcurrentDictionary<EventSourceType, IFooEventSource<T>>();

        public static IFooEventSource<T> GetEventSource(EventSourceType sourceType) => sources.GetOrAdd(sourceType, _ => new FooEventSource<T>());

        public static void Fire(T parameters, params EventSourceType[] sourceTypes) => Fire(parameters, null, sourceTypes);

        public static void Fire(T parameters, EventName[] eventNames = null, params EventSourceType[] sourceTypes)
        {
            foreach (var sourceType in sourceTypes)
            {
                var eventSource = GetEventSource(sourceType);

                eventSource?.FireEventIfUpdated(parameters, eventNames);
            }
        }
    }
}
