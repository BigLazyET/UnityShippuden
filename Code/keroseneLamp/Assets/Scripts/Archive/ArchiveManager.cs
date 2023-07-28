using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using Assets.Scripts.Events.ObserveLikeModeUpgrade;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Archive
{
    public class ArchiveManager : MonoBehaviour
    {
        public Dictionary<ItemType, bool> ItemStateDic = new Dictionary<ItemType, bool>();

        private void OnEnable()
        {
            // PubSubPattern
            EventHandler.BeforeSceneUnload += EventHandler_BeforeSceneUnload;
            EventHandler.AfterSceneLoad += EventHandler_AfterSceneLoad;
            EventHandler.AfterReloadSlotDisplay += EventHandler_AfterReloadSlotDisplay;

            // ObserverPattern
            FooListener<int>.GetEventSource(EventSourceType.BeforeSceneUnload).Subscribe(EventName.ArchiveManager, EventSource_AfterSceneLoad);
            FooListener<int>.GetEventSource(EventSourceType.AfterSceneLoad).Subscribe(EventName.ArchiveManager, EventSource_BeforeSceneUnload);
            FooListener<int>.GetEventSource(EventSourceType.AfterReloadSlotDisplay).Subscribe(EventName.ArchiveManager, EventSource_AfterReloadSlotDisplay);
        }

        private void OnDisable()
        {
            // PubSubPattern
            EventHandler.BeforeSceneUnload -= EventHandler_BeforeSceneUnload;
            EventHandler.AfterSceneLoad -= EventHandler_AfterSceneLoad;
            EventHandler.AfterReloadSlotDisplay -= EventHandler_AfterReloadSlotDisplay;

            // ObserverPattern
            FooListener<int>.GetEventSource(EventSourceType.BeforeSceneUnload).UnSubscribe(EventName.ArchiveManager);
            FooListener<int>.GetEventSource(EventSourceType.AfterSceneLoad).UnSubscribe(EventName.ArchiveManager);
            FooListener<int>.GetEventSource(EventSourceType.AfterReloadSlotDisplay).UnSubscribe(EventName.ArchiveManager);
        }

        #region EventHandler
        private void EventHandler_AfterSceneLoad()
        {
            throw new System.NotImplementedException();
        }

        private void EventHandler_BeforeSceneUnload()
        {
            throw new System.NotImplementedException();
        }

        private void EventHandler_AfterReloadSlotDisplay(int obj)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region EventSource
        private Task EventSource_AfterSceneLoad(int sceneIndex)
        {
            throw new System.NotImplementedException();
        }

        private Task EventSource_BeforeSceneUnload(int sceneIndex)
        {
            // 在场景Unload之前，我们必须保存场景中的道具物体
            //foreach (var item in FindObjectOfType<Item>)
            //{

            //}

            throw new System.NotImplementedException();
        }

        private Task EventSource_AfterReloadSlotDisplay(int itemIndexInSlot)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
