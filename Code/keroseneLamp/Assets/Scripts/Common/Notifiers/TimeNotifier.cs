using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    /*
    * TimeNotifier fires off an event after some duration once the timer has started. The timer can also be configured
    * to automatically restart the timer once the duration has passed or to only trigger once.
    */
    public class TimeNotifier : NotifyBase<TimeNotifierData>
    {
        /*
         * Event will be invoked once duration has passed. If timer is set to restart, it will be invoked every time
         * the duration passes
         */
        public event Action OnNotify;

        private float targetTime;


        public override void Init(TimeNotifierData data)
        {
            base.Init(data);

            targetTime = Time.time + data.duration;
        }

        public override void Tick()
        {
            if (!notifyData.enabled) return;

            if (Time.time < targetTime) return;

            OnNotify?.Invoke();
            targetTime = Time.time + notifyData.duration;
            notifyData.enabled = notifyData.reset;
        }
    }
}
