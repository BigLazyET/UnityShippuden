using Assets.Scripts.Enums;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Scripts.Events.ObserveLikeModeUpgrade
{
    public class FooEventSource<T> : FooEventSourceBase<T>
    {
        public override void Subscribe(EventName eventName, Func<T, Task> observer)
        {
            observers[eventName] = observer;
        }

        public override void UnSubscribe(EventName eventName)
        {
            observers.TryRemove(eventName, out _);
        }

        public override void FireEventIfUpdated(T parameters) => FireEventIfUpdated(parameters, null);

        public override void FireEventIfUpdated(T parameters, params EventName[] eventNames)
        {
            Task.Run(async () =>
            {
                foreach (var observer in observers)
                {
                    if (eventNames != null && !eventNames.Contains(observer.Key))
                        continue;
                    
                    await observer.Value(parameters);
                }
            });
        }

        public void Reset()
        {
            observers.Clear();
        }
    }
}
