using Assets.Scripts.Common;
using Assets.Scripts.Enums;
using Assets.Scripts.Events.ObserveLikeModeUpgrade;
using Assets.Scripts.SO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory
{
    public class InventoryManager : SingletonBase<InventoryManager>
    {
        // 这是数据层面的信息，表示了道具的种类，数量，图片，名称，显示名称等
        // 大部分情况下用作数据配置用，意味着运行时只用作读取而非修改！
        public ItemDataList_SO itemDataList;

        // 这个是实际道具包中含有的道具
        // 这个才是最终要持久化的数据，用于存档读档！
        public List<ItemType> Bag = new List<ItemType>();

        private void OnEnable()
        {
            // remove item in bag, and reload bag ui display
            FooListener<ItemType>.GetEventSource(EventSourceType.UseItem).Subscribe(EventName.InventoryManager, OnUseItem);
            // reload bag ui display
            FooListener<int>.GetEventSource(EventSourceType.AfterSceneLoad).Subscribe(EventName.InventoryManager, OnAfterSceneLoad);
        }

        private void OnDisable()
        {

        }

        private Task OnUseItem(ItemType type)
        {
            Bag.Remove(type);

            if (Bag.Count == 0)
                FooListener<int>.Fire(-1, EventSourceType.ReloadSlotDisplay);
            else
                FooListener<int>.Fire(0, EventSourceType.ReloadSlotDisplay);

            return Task.CompletedTask;
        }

        private Task OnAfterSceneLoad(int sceneIndex)
        {
            return Task.CompletedTask;
        }
    }
}
