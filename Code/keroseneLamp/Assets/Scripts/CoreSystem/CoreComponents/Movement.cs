using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }

        public int FacingDirection { get; private set; }

        public bool CanSetVelocity { get; set; }

        public Vector2 CurrentVelocity { get; private set; }

        public Vector2 workspace;

        protected override void Awake()
        {
            base.Awake();

            //RB = GetComponentInParent<Rigidbody2D>();
            RB = core.Root.GetComponent<Rigidbody2D>();
            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }

        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(velocity * angle.x * direction, velocity * angle.y * direction);
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = velocity * direction;
            SetFinalVelocity();
        }

        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                CurrentVelocity = RB.velocity = workspace;
            }
        }

        public void CheckIfShouldFlip(int inputX)
        {
            if (inputX != 0 && inputX != FacingDirection)
                Flip();
        }

        /// <summary>
        /// 转向
        /// 三种方式：scale, rotate, sprite renderer flip
        /// https://zhuanlan.zhihu.com/p/357525446
        /// </summary>
        public void Flip()
        {
            // scale
            // 确保素材原始scale为1，如果原始存在缩放，则此方法会产生意想不到的效果
            // core.Root.transform.localScale = new Vector3(FacingDirection * -1, 0f, 0f);

            // rotate
            FacingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
            // RB.transform.localRotation = FacingDirection == 1 ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);

            // sprite renderer flip
            // core.Root.GetComponent<SpriteRenderer>().flipX = FacingDirection == -1;
        }
    }
}
