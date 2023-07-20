using System.Collections.Concurrent;

namespace Assets.Scripts.Events.ObserveLikeModeUpgrade
{
    public class FooListener
    {
        public ConcurrentDictionary<string, FooEventSource> sources = new ConcurrentDictionary<string, FooEventSource>();

        public FooEventSource GetEventSource(string sourceType) => sources.GetOrAdd(sourceType, _ => new FooEventSource());

        public void Fire(params string[] sourceTypes)
        {
            foreach (var sourceType in sourceTypes)
            {
                var eventSource = GetEventSource(sourceType);
                eventSource.FireEventIfUpdated(new Foo());
            }
        }
    }
}
