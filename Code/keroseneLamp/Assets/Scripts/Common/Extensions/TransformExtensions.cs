using UnityEngine;

namespace Assets.Scripts.Common.Extensions
{
    public static class TransformExtensions
    {
        public static float AngleFromFacingDirection(this Transform receiver, Transform source, int direction)
        {
            var signedAngle = Vector2.SignedAngle(Vector2.right * direction, (source.position - receiver.position) * direction);
            return signedAngle;
        }
    }
}
