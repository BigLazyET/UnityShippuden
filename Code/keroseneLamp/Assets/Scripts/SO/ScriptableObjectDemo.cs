using Assets.Scripts.Enums;
using Assets.Scripts.Scene;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.SO
{
    public class ScriptableObjectDemo : MonoBehaviour
    {
        private void Start()
        {
            // 通过代码获取ScriptableObject文件时,就通过加载文件的方式,通过路径加载
            var itemDetails = AssetDatabase.LoadAssetAtPath<ItemDataList_SO>(@"Assets/ScriptableObject/blueCube.asset");

            // 通过代码创建并保存ScritableObject的方式 -> 可以放到[InitializeOnLoad]代码逻辑中
            void SaveAsset()
            {
                var itemDetailsListSO = ScriptableObject.CreateInstance<ItemDataList_SO>();
                var itemDetails = new ItemDetails();
                itemDetails.itemType = ItemType.Key;
                itemDetails.itemDisplayName = "钥匙";
                itemDetails.itemIcon = new SpriteRenderer().sprite;
                itemDetailsListSO.itemDetailsList.Add(itemDetails);
                AssetDatabase.CreateAsset(itemDetailsListSO, @"Assets/ScriptableObject/blueCube.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
