using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editors
{
    // 参考链接：[代码自动添加场景到BuildSettings]https://blog.csdn.net/QQhelphelp/article/details/109702537
    // 在启动Unity的时候运行该编辑器脚本 - 在unity loads或者scripts recompiled的时候会运行脚本
    [InitializeOnLoad]
    public class AutoAddScenes
    {
        // 静态构造
        static AutoAddScenes()
        {

            // 获取存放指定场景Scene的文件夹信息
            var scenesDir = new DirectoryInfo(Application.dataPath + "/Scenes");

            // 如果 EditorBuildSettings.scenes 的个数为 0，且存放场景Scene 的文件夹存在，则进入分支
            if (EditorBuildSettings.scenes.Length == 0 && scenesDir.Exists)
            {

                // 把对应场景添加到 EditorBuildSettings 中，并设置是否激活该场景Scene
                EditorBuildSettings.scenes = new EditorBuildSettingsScene[] {

                new EditorBuildSettingsScene("Assets/Scenes/1.unity", true),
                new EditorBuildSettingsScene("Assets/Scenes/2.unity", false),
                new EditorBuildSettingsScene("Assets/Scenes3.unity", false),

                };
            }
        }
    }
}
