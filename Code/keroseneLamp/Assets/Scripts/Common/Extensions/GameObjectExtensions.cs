using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
        {
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }

        /// <summary>
        /// https://blog.csdn.net/boyZhenGui/article/details/128231974
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static bool IsLayerInMask(this GameObject gameObject, LayerMask layerMask)
        {
            return ((1 << gameObject.layer) & layerMask) > 0;
        }
    }
}
