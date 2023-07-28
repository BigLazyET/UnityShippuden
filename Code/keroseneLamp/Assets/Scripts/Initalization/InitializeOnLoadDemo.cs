using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Initalization
{
    /// <summary>
    /// InitializeOnLoad是Unity引擎提供的一种特性，用于在编辑器启动时或脚本重新编译后自动执行指定的操作。
    /// 这种特性非常适合用于在编辑器启动时执行一些初始化操作，以确保项目在启动后能够正常运行
    /// </summary>
    [InitializeOnLoad]
    public static class InitializeOnLoadDemo
    {
        // 静态构造
        static InitializeOnLoadDemo()
        {
            Debug.Log("InitializeOnLoad Foo called");
        }
    }
}
