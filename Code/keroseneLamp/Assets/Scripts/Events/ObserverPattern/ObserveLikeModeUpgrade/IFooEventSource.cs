using Assets.Scripts.Enums;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Assets.Scripts.Events.ObserveLikeModeUpgrade
{
    public interface IFooEventSource { }

    public interface IFooEventSource<T> : IFooEventSource
    {
        void FireEventIfUpdated(T parameters);
        
        void FireEventIfUpdated(T parameters, params EventName[] eventNames);

        void Subscribe(EventName eventName, Func<T, Task> observer);

        void UnSubscribe(EventName eventName);
    }

    public abstract class FooEventSourceBase<T> : IFooEventSource<T>
    {
        protected ConcurrentDictionary<EventName, Func<T, Task>> observers = new ConcurrentDictionary<EventName, Func<T, Task>>();

        public abstract void FireEventIfUpdated(T parameters);

        public abstract void FireEventIfUpdated(T parameters, params EventName[] eventNames);

        public abstract void Subscribe(EventName eventName, Func<T, Task> observer);

        public abstract void UnSubscribe(EventName eventName);
    }
}
