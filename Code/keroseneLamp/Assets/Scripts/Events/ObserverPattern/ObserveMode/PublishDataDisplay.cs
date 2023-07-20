using System;

namespace Assets.Scripts.Events
{
    public class PublishDataDisplay : IObserver<PublishData>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(PublishData value)
        {
            throw new NotImplementedException();
        }
    }
}
