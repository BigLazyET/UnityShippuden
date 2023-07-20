using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Assets.Scripts.Events.ObserveLikeMode
{
    public class EventSource : IEventSource
    {
        private ConcurrentDictionary<Func<string, object, Task>, Func<string, object, Task>> observers = new ConcurrentDictionary<Func<string, object, Task>, Func<string, object, Task>>();

        public void Notify(string eventName, object parameters)
        {
            Task.Run(async () =>
            {
                foreach (var observer in observers.Keys)
                {
                    await observer(eventName, parameters);
                }
            });
        }

        public IDisposable Subscribe(Func<string, object, Task> observer)
        {
            observers[observer] = observer;
            return new Disposable(() => observers.TryRemove(observer, out _));
        }

        private class Disposable : IDisposable
        {
            private Action action;
            public Disposable(Action action)
            {
                this.action = action;
            }
            public void Dispose() => action();
        }
    }
}
