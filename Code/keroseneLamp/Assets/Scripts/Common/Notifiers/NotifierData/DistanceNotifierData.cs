using UnityEngine;

namespace Assets.Scripts.Common
{
    public class DistanceNotifierData : NotifierData
    {
        public Vector3 referencePos;
        public float desiredDistance;
        public bool checkInSide;
    }
}
