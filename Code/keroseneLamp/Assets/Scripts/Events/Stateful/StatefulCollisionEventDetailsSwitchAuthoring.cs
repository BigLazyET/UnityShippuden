using UnityEngine;

namespace Events
{
    public class StatefulCollisionEventDetailsSwitchAuthoring : MonoBehaviour
    {
        [Tooltip("If selected, the details will be calculated in collision event dynamic buffer of this entity")]
        public bool CalculateDetails = false;
    }
}
