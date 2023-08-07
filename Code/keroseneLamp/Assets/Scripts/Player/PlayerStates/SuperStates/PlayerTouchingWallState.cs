using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    public class PlayerTouchingWallState : PlayerState
    {
        // 输入相关 - 由 PlayerInputHandler负责
        protected int xInput;
        protected int yInput;
        protected bool jumpInput;
        protected bool grapInput;

        // 碰撞相关 - 由CollisionSenses负责，此类型为CoreComponent
        protected bool isGrounded;
        protected bool isTouchingWall;
        protected bool isTouchingLedge;

        protected virtual Movement Movement => Movement ?? core.GetCoreComponent<Movement>();

        public CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        public override PlayerStateType PlayerStateType => PlayerStateType.TouchingWall;

        public PlayerTouchingWallState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

            if(CollisionSenses)
            {
                isGrounded = CollisionSenses.Ground;
                isTouchingWall = CollisionSenses.WallFront;
                isTouchingLedge = CollisionSenses.LedgeHorizontal;
            }

            if (isTouchingWall && !isTouchingLedge)
                stateMachine.GetState<PlayerLedgeClimbState>().SetDetectedPosition(player.transform.position);
        }

        public override void Enter() => base.Enter();

        public override void Exit() => base.Exit();

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;
            grapInput = player.InputHandler.GrapInput;

            if (jumpInput)
            {
                stateMachine.GetState<PlayerWallJumpState>().DetermineWallJumpDirection();
                stateMachine.ChangeState(PlayerStateType.WallJump);
            }
            else if (isGrounded && !grapInput)
                stateMachine.ChangeState(PlayerStateType.Idle);
            else if (!isTouchingWall || (xInput != Movement.FacingDirection && !grapInput))
                stateMachine.ChangeState(PlayerStateType.InAir);
            else if (isTouchingWall && isTouchingLedge)
                stateMachine.ChangeState(PlayerStateType.LedgeClimb);   // TODO?
        }

        public override void PhysicsUpdate() => base.PhysicsUpdate();

        public override void AnimationTrigger() => base.AnimationTrigger();

        public override void AnimationFinishTrigger() => base.AnimationFinishTrigger();
    }
}
