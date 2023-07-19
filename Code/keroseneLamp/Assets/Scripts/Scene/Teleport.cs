using Assets.Scripts.Editors;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    // 此类需要挂载到子场景
    // 子场景，如关卡页面，背包页面，按键页面等等
    public class Teleport : MonoBehaviour
    {
        [SceneName] public string fromSceneName;
        [SceneName] public string toSceneName;

        public void TeleportToScene()
        {
            TeleportManager.Instance.Transition(fromSceneName, toSceneName);
        }
    }
}
