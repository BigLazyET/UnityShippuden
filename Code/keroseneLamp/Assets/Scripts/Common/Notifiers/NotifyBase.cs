namespace Assets.Scripts.Common
{
    public class NotifyBase
    {
       
    }

    public abstract class NotifyBase<T> : NotifyBase where T : NotifierData
    {
        protected T notifyData;

        public virtual void Init(T data)
        {
            notifyData = data;

            notifyData.enabled = true;
        }

        public abstract void Tick();

        public virtual void Disable() => notifyData.enabled = false;
    }
}
