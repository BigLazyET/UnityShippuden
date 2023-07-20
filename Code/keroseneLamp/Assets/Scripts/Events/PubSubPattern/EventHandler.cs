using Assets.Scripts.Scene;
using System;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public static class EventHandler
    {
        // 物品槽
        public static event Action<ItemDetails, int> ReloadSlotDisplay;
        public static event Action<int> AfterReloadSlotDisplay;
        public static event Action<ItemDetails, bool> ItemSelected;
        public static event Action<ItemName> UseItem;
        public static event Action<int> BagChangeItem;

        // 对话
        public static event Action<string, GameObject> DialoguePlay;
        public static event Action DialogueDone;

        // 场景
        public static event Action BeforeSceneUnload;
        public static event Action AfterSceneLoad;

        public static void CallBeforeSceneUnloadEvent()
        {
            BeforeSceneUnload?.Invoke();
        }

        public static void CallAfterSceneLoadEvent()
        {
            AfterSceneLoad?.Invoke();
        }
    }
}
