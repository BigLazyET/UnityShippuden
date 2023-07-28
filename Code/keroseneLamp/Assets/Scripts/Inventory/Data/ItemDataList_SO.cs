using Assets.Scripts.Enums;
using Assets.Scripts.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList_SO")]
    public class ItemDataList_SO : ScriptableObject
    {
        public List<ItemDetails> itemDetailsList;

        public ItemDetails FindItemDetails(ItemType itemName)
        {
            var itemDetails = itemDetailsList.Find(i => i.itemType == itemName);
            return itemDetails;
        }
    }
}
