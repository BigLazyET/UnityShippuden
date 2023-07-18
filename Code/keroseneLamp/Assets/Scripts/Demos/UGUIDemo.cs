using UnityEngine;

namespace Assets.Scripts.UGUIDemo
{
    /// <summary>
    /// UGUI 是 Unity 官方的 UI 实现方式
    /// 三要素：Rect Transform，Canvas Renderer，EventSystem
    /// </summary>
    public class UGUIDemo : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            // Canvas Group组件是用于控制UI元素在Canvas中的显示和隐藏的组件
            // 同时可以决定UI元素的透明度，可交互性，阻止鼠标和触摸事件的穿透等
        }
    }
}
