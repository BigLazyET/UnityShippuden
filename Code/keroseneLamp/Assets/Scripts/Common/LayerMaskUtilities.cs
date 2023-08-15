using UnityEngine;

namespace Assets.Scripts.Common
{
    /// <summary>
    /// This class holds some useful functions pertaining to LayerMasks
    /// 原理：https://blog.csdn.net/boyZhenGui/article/details/128231974
    /// </summary>
    public static class LayerMaskUtilities
    {

        /// <summary>
        /// This function uses bit shifting to determine if a particular layer is contained within a LayerMask
        /// </summary>
        public static bool IsLayerInMask(int layer, LayerMask mask) => ((1 << layer) & mask) > 0;

        public static bool IsLayerInMask(RaycastHit2D hit, LayerMask mask) =>
            IsLayerInMask(hit.collider.gameObject.layer, mask);
    }
}
