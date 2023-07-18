using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editors
{
    // 参考链接：[Unity编辑器开发之PropertyDrawer]https://blog.csdn.net/qq_42139931/article/details/116779815

#if UNITY_EDITOR
    /// <summary>
    /// 目的是：根据设定的SceneName，自动计算出其在全局的SceneIndex
    /// 用SceneIndex比用SceneName更为健壮
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class SceneNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneNameAttr = attribute as SceneNameAttribute;
            var sceneNames = sceneNameAttr.SceneNames;

            if (property.propertyType == SerializedPropertyType.String)
            {
                // property.name -> [Scene] public string fromScene -> 在这里就是 property.name = fromScene
                // property.[Type]value -> 一般而言是 上述 fromScene=value的[value]值，但是这边需要做一个转换，转换成全局的SceneIndex
                var index = Mathf.Max(0, Array.IndexOf(sceneNames, property.stringValue));
                index = EditorGUI.Popup(position, property.name, index, sceneNames);
                property.stringValue = sceneNames[index];
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                // 下拉框
                property.intValue = EditorGUI.Popup(position, property.name, property.intValue, sceneNames);
            }
            else
            {
                base.OnGUI(position, property, label);
            }
        }
    }
#endif
}
