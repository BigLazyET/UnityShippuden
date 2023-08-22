using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 用来标记 Optional Sprite GameObject，这个GameObject必须在BaseWeaponObject内，提供其SpriteRenderer组件
    /// 
    /// </summary>
    public class OptionalSpriteMarker : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    }
}
