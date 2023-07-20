using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Assets.Scripts.Events.ObserveLikeModeUpgrade
{
    public class FooEventSource
    {
        Foo _lastArgs;  // 用作判定是否需要执行Fire

        readonly ConcurrentBag<Action<Foo>> _observers = new ConcurrentBag<Action<Foo>>();

        public void Subscribe(Action<Foo> observer)
        {
            var lastArgs = Volatile.Read(ref _lastArgs);
            if (lastArgs != null) observer(lastArgs);

            _observers.Add(observer);
        }

        public void FireEventIfUpdated(Foo foo)
        {
            if (!CheckIsNeedUpdate(foo)) return;
            foreach (var observer in _observers) observer(_lastArgs);
        }

        public void FireEventTaskDeleted()
        {
            _lastArgs = new Foo { IsExist = false };
            foreach (var observer in _observers) observer(_lastArgs);
        }

        public void Reset()
        {
            _lastArgs = null;
            _observers.Clear();
        }

        private bool CheckIsNeedUpdate(Foo foo)
        {
            // 如果取不到当前的话，就保留上一个不更新
            if (foo == null) return false;

            // TODO: compare to decide need update or not


            _lastArgs = foo;
            return true;
        }

    }
}
