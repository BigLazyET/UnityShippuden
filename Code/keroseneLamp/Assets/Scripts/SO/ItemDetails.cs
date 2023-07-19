using System;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ItemDetails : ScriptableObject
    {
        public ItemName itemName;

        public string itemDisplayName;

        public Sprite itemIcon;
    }
}
