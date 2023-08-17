using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
        {
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }
    }
}
