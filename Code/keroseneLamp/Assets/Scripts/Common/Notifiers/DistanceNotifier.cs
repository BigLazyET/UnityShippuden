using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    /// <summary>
    /// Distance notifier takes in a starting position and a desired distance from that position. When an object
    /// reaches that distance from the target, it invokes an event.
    /// 距离通知器
    /// </summary>
    public class DistanceNotifier : NotifyBase<DistanceNotifierData>
    {
        /// <summary>
        /// This event is broadcast whenever the distance condition is met. If checkInside is true then event will be broadcast if the current distance
        /// is less than distance, otherwise it will broadcast if it is greater. It will also broadcast continuously while enabled is true
        /// </summary>
        public event Action<float> OnNotify;

        private float currentSqrDistance;

        public override void Init(DistanceNotifierData data)
        {
            base.Init(data);

            currentSqrDistance = (notifyData.referencePos - notifyData.transform.position).sqrMagnitude;
        }

        public override void Tick()
        {
            if (!notifyData.enabled)
                return;

            // We are using the square of distances as square root function is expensive
            var sqrDistance = Mathf.Pow(notifyData.desiredDistance, 2);

            if (notifyData.checkInSide && sqrDistance < currentSqrDistance) return;
            if (!notifyData.checkInSide && sqrDistance > currentSqrDistance) return;
            // Pass current distance to function stored within the Action. Avoids having to do an if else check every tick and instead moves that check to constructor.
            OnNotify.Invoke(currentSqrDistance);
            currentSqrDistance = (notifyData.referencePos - notifyData.transform.position).sqrMagnitude;
            notifyData.enabled = notifyData.reset;
        }
    }
}
