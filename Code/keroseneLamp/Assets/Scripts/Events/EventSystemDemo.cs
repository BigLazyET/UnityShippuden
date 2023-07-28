using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events
{
    // 一般而言，不需要使用自带的 UI EventSystem，使用C# Action更好
    // 参考链接：https://www.jianshu.com/p/229d9abc7bd9

    /// <summary>
    /// 声明一个自定义Handler，继承IEventSystemHandler
    /// </summary>
    public interface ICustomMessageTarget : IEventSystemHandler
    {
        // functions that can be called via the messaging system
        void Message1();
        void Message2();
    }

    /// <summary>
    /// 实现上述接口，并把这个脚本挂载到物体AAA上
    /// </summary>
    public class CustomMessageTarget : MonoBehaviour, ICustomMessageTarget
    {
        public void Message1()
        {
            Debug.Log("Message 1 received");
        }

        public void Message2()
        {
            Debug.Log("Message 2 received");
        }
    }

    /// <summary>
    /// 在任何脚本中使用ExecuteEvents静态类发送Message，来执行接口中定义的方法
    /// </summary>
    public class EventSystemDemo : MonoBehaviour
    {
        //target should be AAA
        public GameObject target;

        private void Start()
        {
            // 第一个参数是发送message到的gameobject对象，只有当该对象上有IEventSystemHandler实现类的时候才可以，这个例子中就是物体AAA
            ExecuteEvents.Execute<ICustomMessageTarget>(target, null, (x, y) => x.Message1());
            // ExecuteEvents静态类还有其他方法

            // EventSystems.ExecuteEvents.CanHandleEvent Can the given GameObject handle the IEventSystemHandler of type T.
            // EventSystems.ExecuteEvents.Execute Execute the event of type T : IEventSystemHandler on GameObject.
            // EventSystems.ExecuteEvents.ExecuteHierarchy Recurse up the hierarchy calling Execute<T> until there is a GameObject that can handle the event. 
            // EventSystems.ExecuteEvents.GetEventHandler Traverse the object hierarchy starting at root, and return the GameObject which implements the event handler of type<T>
            // EventSystems.ExecuteEvents.ValidateEventData  Attempt to convert the data to type T. If conversion fails an ArgumentException is thrown.

        }
}
}
