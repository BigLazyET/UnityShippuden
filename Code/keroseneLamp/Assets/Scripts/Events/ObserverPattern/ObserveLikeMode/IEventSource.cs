using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Events.ObserveLikeMode
{
    public interface IEventSource
    {
        // We dont need specific the observer class， we just need OnNext then just think it as a Task
        // We just define the Subscribe function to implement IObserver-Like Mode
        IDisposable Subscribe(Func<string, object, Task> observer);

        void Notify(string eventName, object parameters);
    }
}
