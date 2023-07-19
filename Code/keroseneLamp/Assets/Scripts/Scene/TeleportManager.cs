using Assets.Scripts.Common;
using Assets.Scripts.Editors;
using Assets.Scripts.Events;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scene
{
    // 参考链接：https://blog.csdn.net/float_freedom/article/details/126221260
    // 参考链接：https://github.com/andongni0723/Isoland2-Study/tree/main
    // 此类需要挂载到基础场景
    // 基础场景：可以理解为主界面，上面放置关卡列表，道具入口，角色技能等 -> 挂载TransitionManager
    public class TeleportManager : SingletonBase<TeleportManager>
    {
        private bool isFade;

        public float fadeDuration;
        public CanvasGroup canvasGroup;

        [SceneName] public string firstStartScene;

        private void OnEnable()
        {
            EventHandler.BeforeSceneUnload += EventHandler_BeforeSceneUnload;
            EventHandler.AfterSceneLoad += EventHandler_AfterSceneLoad;
        }

        private void EventHandler_AfterSceneLoad()
        {
            Debug.Log("EventHandler_AfterSceneLoad");
        }

        private void EventHandler_BeforeSceneUnload()
        {
            Debug.Log("EventHandler_BeforeSceneUnload");
        }

        private void OnDisable()
        {
            EventHandler.BeforeSceneUnload -= EventHandler_BeforeSceneUnload;
            EventHandler.AfterSceneLoad -= EventHandler_AfterSceneLoad;
        }

        private void Start()
        {
            Transition(string.Empty, firstStartScene);
        }

        public void Transition(string fromSceneName, string toSceneName)
        {
            if (!isFade)
            {
                StartCoroutine(TransitionToScene(fromSceneName, toSceneName));
            }
        }

        private IEnumerator TransitionToScene(string fromSceneName, string toSceneName)
        {
            // Fade in
            yield return Fade(1);

            // Unload scene
            if(!string.IsNullOrWhiteSpace(fromSceneName))
            {
                EventHandler.CallBeforeSceneUnloadEvent();
                yield return SceneManager.UnloadSceneAsync(fromSceneName);
            }

            // Load scene
            yield return SceneManager.LoadSceneAsync(toSceneName);

            var newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);

            EventHandler.CallAfterSceneLoadEvent();

            // Fade out
            yield return Fade(0);
        }

        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            canvasGroup.blocksRaycasts = true;

            // Use DOTween
            yield return canvasGroup.DOFade(targetAlpha, fadeDuration).WaitForCompletion();

            // Use Unity Native Animation with Mathf
            var speed = Mathf.Abs(targetAlpha - canvasGroup.alpha) / fadeDuration;
            while(!Mathf.Approximately(targetAlpha, canvasGroup.alpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            }

            canvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}
