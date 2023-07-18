using UnityEngine;

namespace Assets.Scripts.DebugDemo
{
    public class DebugDemo : MonoBehaviour
    {
        public Transform groundCheck;
        public float groundCheckRadius;

        public Vector3 offset;
        public float length;
        public LayerMask whatIsGround;
        public bool isHit;

        private void Update()
        {
            // Debug.DrawRay
            isHit = !isHit;
            Color color = isHit ? Color.red : Color.green;
            Debug.DrawRay(transform.position+ offset, transform.forward * length, color, 0.2f);
        }

        /// <summary>
        /// 在程序一运行就执行，之后每帧都在执行
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        /// <summary>
        /// 在鼠标点击到脚本挂载的物体的身上的时候运行，不管有多少父类对象，它都会执行
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
