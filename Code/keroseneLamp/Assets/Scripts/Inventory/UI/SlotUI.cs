using Assets.Scripts.Common;
using Assets.Scripts.Events;
using Assets.Scripts.Scene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.UI
{
    /// <summary>
    /// 此示例采用unity UGUI自带的EventSystem，针对鼠标的操作（局限性）
    /// 
    /// 实际游戏中使用的UI操作，可能只涉及到gamepad和keyboard，所以必须要一个通用的方式
    /// 后续我们可以采用Input System UI Input Module的形式
    /// </summary>
    public class SlotUI : SingletonBase<SlotUI>, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image itemIcon;  // 道具图片
        public ItemDetails currentItem; // 当前道具槽中的道具
        public ItemTooltip itemTooltip; // 当前道具的tooltip
        [SerializeField] private bool isSelected;

        public void DisplayItem(ItemDetails itemDetails, int index)
        {
            currentItem = itemDetails;
            gameObject.SetActive(true);

            itemIcon.sprite = currentItem.itemIcon;
            itemIcon.SetNativeSize();
        }

        public void SetEmpty()
        {
            gameObject.SetActive(false);
            itemIcon.sprite = null;
            currentItem = null;
            isSelected = false;
        }

        public ItemDetails GetSelectedItem()
        {
            if (isSelected)
                return currentItem;
            return new ItemDetails();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isSelected = !isSelected;
            EventHandler.CallItemSelected(currentItem, isSelected);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            itemTooltip.SetText(currentItem.itemDisplayName);
            itemTooltip.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemTooltip.gameObject.SetActive(false);
        }
    }
}
