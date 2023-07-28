using UnityEngine;

namespace Assets.Scripts.Initalization
{
    /// <summary>
    /// RuntimeInitializeLoadType参数
    /// RuntimeInitializeOnLoadMethod方法标记的参数可以指定方法调用的时机：
    /// AfterSceneLoad：场景载入后调用
    /// BeforeSceneLoad：场景载入前调用
    /// AfterAssembliesLoaded：在所有程序集载入后，预加载资源(preloaded assets)初始化后调用
    /// BeforeSplashScreen：在启动画面显示后立即调用
    /// SubsystemRegistration：在注册子系统时调用
    /// </summary>
    public class RuntimeInitializeOnLoadMethodDemo
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            // 在第一个场景的Awake方法调用前输出字符串Before first Scene loaded
            Debug.Log("Before first Scene loaded");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnAfterSceneLoadRuntimeMethod()
        {
            // 在第一个场景的Awake方法调用后，Start方法调用前输出字符串After first Scene loaded
            Debug.Log("After first Scene loaded");
        }

        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {
            // 在第一个场景的Awake方法调用后，Start方法调用前输出字符串RuntimeMethodLoad: After first Scene loaded
            Debug.Log("RuntimeMethodLoad: After first Scene loaded");
        }
    }
}
