using Assets.Scripts.Common;
using Assets.Scripts.Editors;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Scene
{
    // 参考链接：https://blog.csdn.net/float_freedom/article/details/126221260
    // 参考链接：https://github.com/andongni0723/Isoland2-Study/tree/main
    // 基础场景：可以理解为主界面，上面放置关卡列表，道具入口，角色技能等 -> 挂载TransitionManager
    public class TransitionManager : SingletonBase<TransitionManager>
    {
        private bool isFade;

        public float fadeDuration;
        public bool canTransition;
        public CanvasGroup canvasGroup;

        [SceneName] public string firstStartScene;

        private void Start()
        {
            Transition(string.Empty, firstStartScene);
        }

        public void Transition(string fromSceneName, string toSceneName)
        {
            if (!isFade)
            {

            }
        }

        private IEnumerator TransitionToScene(string fromSceneName, string toSceneName)
        {
            // Fade in
            //yield return 
        }

        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            canvasGroup.blocksRaycasts = true;

            //yield return canvasGroup.

            canvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}
