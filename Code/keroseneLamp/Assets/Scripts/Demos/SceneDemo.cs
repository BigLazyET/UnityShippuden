using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneDemo
{
    public class SceneDemo : MonoBehaviour
    {
        public Transform retain;

        private void Awake()
        {
            // 加载关卡
            SceneManager.LoadScene(1); //1是场景的索引
            SceneManager.LoadScene("sceneName");
            // 获取当前关卡
            var scene = SceneManager.GetActiveScene();

            // 切换场景会默认销毁当前场景中的所有游戏对象，若不想销毁某对象，可以调用 MonoBehaviour 的 DontDestroyOnLoad 方法，如下：
            DontDestroyOnLoad(retain);
        }
    }
}
