using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.UI
{
    public class ItemTooltip : MonoBehaviour
    {
        public Text itemDisplayName;

        public void SetText(string displayName)
        {
            itemDisplayName.text = displayName;
        }
    }
}
