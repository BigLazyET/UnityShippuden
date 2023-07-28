using Assets.Scripts.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    /// <summary>
    /// 道具物品对象
    /// </summary>
    [Serializable]
    public class ItemDetails
    {
        public ItemType itemType;

        public string itemDisplayName;

        public Sprite itemIcon;
    }
}
