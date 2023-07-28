using Assets.Scripts.Enums;
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
        public static event Action<ItemType> UseItem;
        public static event Action<int> BagChangeItem;

        public static void CallReloadSlotDisplay(ItemDetails itemDetails, int itemIndex) => CallReloadSlotDisplay(itemDetails, itemIndex);
        public static void CallAfterReloadSlotDisplay(int itemIndex) => CallAfterReloadSlotDisplay(itemIndex);
        public static void CallItemSelected(ItemDetails itemDetails, bool isSelected) => ItemSelected?.Invoke(itemDetails, isSelected);
        public static void CallUseItem(ItemType itemType) => UseItem?.Invoke(itemType);
        public static void CallBagChangeItem(int itemIndex) => BagChangeItem?.Invoke(itemIndex);

        // 对话
        public static event Action<string, GameObject> DialoguePlay;
        public static event Action DialogueDone;

        public static void CallDialoguePlay(string title, GameObject dialogue) => DialoguePlay?.Invoke(title, dialogue);
        public static void CallDialogueDone() => DialogueDone?.Invoke();

        // 场景
        public static event Action BeforeSceneUnload;
        public static event Action AfterSceneLoad;

        public static void CallBeforeSceneUnloadEvent() => BeforeSceneUnload?.Invoke();

        public static void CallAfterSceneLoadEvent() => AfterSceneLoad?.Invoke();
    }
}
