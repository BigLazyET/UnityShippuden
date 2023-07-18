using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ColliderDetectionDemo
{
    public class ColliderDetectionDemo : MonoBehaviour
    {
        public Transform groundCheck;
        public float groundCheckRadius;
        public LayerMask whatIsGround;

        public Vector3 offset;
        public float length;
        public bool isHit;
        public float minLayerDep;
        public float maxLayerDep;

        private Animator animator;

        private void Update()
        {
            animator = gameObject.GetComponent<Animator>();

            // --- 粗粒度的碰撞检测 ---

            // Animation + Event - 动画帧绑事件处理 => 这边是代码实现，只存在于运行时；如果要预览时呈现需要在编辑器editor中animation动画界面添加event
            var deomoAnimationClip = animator.runtimeAnimatorController.animationClips.First(x => x.name == "demo");
            deomoAnimationClip.AddEvent(new AnimationEvent { time = 24f / 30, functionName = "Foo" });

            // 检查 Collider 是否落在圆形区域内。圆由其在世界空间中的中心坐标及其半径定义。可选的 layerMask 允许测试仅检查特定层上的对象
            var collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            // 形如：Physics2D.OverlapXXX的方法

            // --- 高精度的碰撞检测 ---
            Physics2D.Raycast(transform.position + offset, transform.forward, length, whatIsGround, minLayerDep, maxLayerDep);
        }

        void Foo()
        {
            // 动画第24帧 碰撞检测
            print("被触发");
        }
    }
}
