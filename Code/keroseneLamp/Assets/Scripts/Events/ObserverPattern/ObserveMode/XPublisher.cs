using System;
using System.Collections.Concurrent;

namespace Assets.Scripts.Events
{
    public class XPublisher : IObservable<PublishData>
    {
        public ConcurrentDictionary<IObserver<PublishData>, IObserver<PublishData>> observers;

        public IDisposable Subscribe(IObserver<PublishData> observer)
        {
            observers[observer] = observer;

            return new Disposable(() => observers.TryRemove(observer, out _));
        }

        private void Notify(PublishData publishData)
        {
            foreach (var observer in observers)
            {
                observer.Value.OnNext(publishData);
            }
        }

        private class Disposable : IDisposable
        {
            private Action action;
            public Disposable(Action action) { 
                this.action = action;
            }
            public void Dispose() => action();
        }
    }
}
