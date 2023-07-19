using UnityEngine;

namespace Assets.Scripts.Scene
{
    public class CursorManager : MonoBehaviour
    {
        private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /// <summary>
        /// 物体是否与鼠标碰撞
        /// 鼠标是否在物体范围之内
        /// </summary>
        /// <returns></returns>
        private Collider2D MouseInGameobject()
        {
            return Physics2D.OverlapPoint(mouseWorldPos);
        }

        private void Update()
        {
            if (!MouseInGameobject())
                return;

            // deal with mouse click
            if (!Input.GetMouseButtonDown(0))
                return;

            DoClick(MouseInGameobject().gameObject);

        }

        /// <summary>
        /// 点击物体的反馈
        /// 这里是做跳转
        /// </summary>
        /// <param name="target"></param>
        private void DoClick(GameObject target)
        {
            if(target == null) return;

            // 这边最好可以给挂载Teleport的GameObject设定tag，从而通过判定tag来操作，如下
            //switch(target.tag)
            //{
            //    case "Teleport":    // 假设tag=Teleport
            //        var teleport = target.GetComponent<Teleport>();
            //        teleport.TeleportToScene();
            //        break;
            //}

            // 以上是只存在一个Teleport，如果针对Click有多个操作，其中一个为Teleport而已，则如下
            foreach (var teleport in target.GetComponents<Teleport>())
            {
                teleport.TeleportToScene();
            }
        }
    }
}
