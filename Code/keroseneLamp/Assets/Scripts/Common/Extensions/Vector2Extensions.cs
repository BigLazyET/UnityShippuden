using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Vector2Extensions
    {
        public static Quaternion Vector2ToRotation(this Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);
            return rotation;
        }
    }
}
