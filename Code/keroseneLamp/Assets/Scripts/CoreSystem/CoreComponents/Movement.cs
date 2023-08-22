using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB {  get; private set; }

        public int FacingDirection { get; private set; }

        public bool CanSetVelocity { get; set; }

        public Vector2 CurrentVelocity { get; private set; }

        public Vector2 workspace;

        public override void Init()
        {
            base.Init();

            RB = GetComponentInParent<Rigidbody2D>();
            // RB = core.transform.parent.GetComponent<Rigidbody2D>();
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
            if(CanSetVelocity)
            {
                CurrentVelocity = RB.velocity = workspace;
            }
        }

        public void CheckIfShouldFlip(int inputX)
        {
            if (inputX != 0 && inputX != FacingDirection)
                Flip();
        }

        public void Flip()
        {
            FacingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
