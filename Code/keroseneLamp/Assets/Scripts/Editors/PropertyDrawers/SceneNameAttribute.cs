using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editors
{
    public class SceneNameAttribute : PropertyAttribute
    {
        public string[] SceneNames => AllSceneNames();

        private static string[] AllSceneNames()
        {
            var sceneNames = new List<string>();

            // Build Settings：https://docs.unity3d.com/cn/560/Manual/BuildSettings.html
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    // scene.path：形如 Assets/Scenes/1.unity
                    var name = scene.path.Substring(scene.path.LastIndexOf('/'));   // 1.unity
                    name = name.Substring(0, name.Length - 6);  // 1
                    sceneNames.Add(name);
                }
            }
            return sceneNames.ToArray();
        }
    }
}
