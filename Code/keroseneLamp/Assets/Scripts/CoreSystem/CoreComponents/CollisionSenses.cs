using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    /// <summary>
    /// 碰撞检测
    /// 适用于大部分物体的基本碰撞检测判断：地面判断，墙壁判断，天花板判断等
    /// </summary>
    public class CollisionSenses : CoreComponent
    {
        public Movement Movement => Movement ?? core.GetCoreComponent<Movement>();

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;    // TODO?
        [SerializeField] private Transform ledgeCheckVertical;  // TODO?
        [SerializeField] private Transform ceilingCheck;

        [SerializeField] private float groundedCheckRadius;
        [SerializeField] private float wallCheckDistance;

        [SerializeField] private LayerMask whatIsGround;

        public float GroundedCheckRadius { get => groundedCheckRadius; set => groundedCheckRadius = value; }

        public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }

        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

        public Transform GroundCheck { get => groundCheck ?? default; private set => groundCheck = value; }

        public Transform WallCheck { get => wallCheck ?? default; private set => wallCheck = value; }

        public Transform LedgeCheckHorizontal { get => ledgeCheckHorizontal ?? default; private set => ledgeCheckHorizontal = value; }

        public Transform LedgeCheckVertical { get => ledgeCheckVertical ?? default; private set => ledgeCheckVertical = value; }

        public Transform CeilingCheck { get => ceilingCheck ?? default; private set => ceilingCheck = value; }

        public bool Ceiling => Physics2D.OverlapCircle(CeilingCheck.position, GroundedCheckRadius, whatIsGround);

        public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, GroundedCheckRadius, whatIsGround);

        public bool WallFront => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, WallCheckDistance, WhatIsGround);

        public bool WallBack => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, WallCheckDistance, WhatIsGround);

        public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, WallCheckDistance, WhatIsGround);

        public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, WallCheckDistance, WhatIsGround);
    }
}
