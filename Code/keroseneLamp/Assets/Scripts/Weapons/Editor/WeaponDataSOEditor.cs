using Assets.Scripts.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        public ReorderableList datas;

        private WeaponDataSO weaponDataSO;
        private static List<Type> ComponetDataTypes = new List<Type>();

        private void OnEnable()
        {
            weaponDataSO = target as WeaponDataSO;

            datas = new ReorderableList(serializedObject, serializedObject.FindProperty("ComponentDatas"), true, true, true, true);

            datas.onAddDropdownCallback = OnAddDropdownCallback;
        }

        private void OnAddDropdownCallback(Rect btn, ReorderableList list)
        {
            var menu = new GenericMenu();

            foreach (var item in ComponetDataTypes)
            {
                menu.AddItem(new GUIContent(item.Name), false, ClickHandler, item);
            }

            menu.ShowAsContext();
        }

        private void ClickHandler(object userData)
        {
            var componentData = Activator.CreateInstance((Type)userData) as ComponentData;
            if (componentData == null) return;
            componentData.InitializeAttackData(weaponDataSO.NumberOfAttacks);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            datas.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        [DidReloadScripts]
        private static void ReCompile()
        {
            var types = from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a=>a.GetTypes())
                        where typeof(ComponentData).IsAssignableFrom(type) && !type.ContainsGenericParameters && type.IsClass
                        select type;
        }
    }
}
