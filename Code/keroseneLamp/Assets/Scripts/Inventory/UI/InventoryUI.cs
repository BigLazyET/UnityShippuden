using Assets.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.UI
{
    /// <summary>
    /// 此类的作用是item Holder，相当于是很多Child GameObject的Parent GameObject
    /// 在Editor Hierarchy中显示为下面会有LeftButton，RightButton，SlotUI，Itemtootip，ItemDisplayText等GameObject
    /// 
    /// 其本身有一个道具栏的Image Component，同时挂载本Script代码，其为包含的UI GameObject提供一些事件，比如LeftButton的按钮点击事件等
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        // 以下为本物体GameObject下包含的GameObject，其在Hierarchy窗口就表示为父物体和子物体的关系
        public Button leftButton;
        public Button rightButton;
        public SlotUI slotUI;
        public ItemTooltip itemTooltip; // GameObject?
        public TextMeshProUGUI itemDisplayText;

        public int currentIndex;

        private void OnEnable()
        {
            EventHandler.ReloadSlotDisplay += EventHandler_ReloadSlotDisplay;
        }

        private void OnDisable()
        {
            EventHandler.ReloadSlotDisplay -= EventHandler_ReloadSlotDisplay;
        }
        
        private void EventHandler_ReloadSlotDisplay(Scene.ItemDetails itemDetails, int itemIndex)
        {
            if(itemIndex == -1)
            {
                slotUI.SetEmpty();
                currentIndex = -1;
                leftButton.interactable = rightButton.interactable = false;
            }
            else
            {
                slotUI.DisplayItem(itemDetails, itemIndex);
                currentIndex = itemIndex;
                ButtonInteractableCheck();
            }
        }

        // 留作LeftButton和RightButton在Editor上配置OnClick方法
        public void ButtonClickChangeItem(int stepr)
        {
            currentIndex += stepr;
            ButtonInteractableCheck();
            EventHandler.CallBagChangeItem(currentIndex);
        }

        private void ButtonInteractableCheck()
        {
            var bagItemsCount = InventoryManager.Instance.Bag.Count;

            leftButton.interactable = currentIndex > 0;
            rightButton.interactable = currentIndex < bagItemsCount - 1;
        }
    }
}
