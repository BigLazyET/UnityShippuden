using Assets.Scripts.Core;
using Assets.Scripts.Player.Enums;
using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// 所有可以在地面进行的状态的基状态
    /// 具体相关的状态由：Idle, Land, Move, CrouchIdle, CrouchMove
    /// </summary>
    public class PlayerGroundedState : PlayerState
    {
        // 输入相关 - 由 PlayerInputHandler负责
        protected int xInput;
        protected int yInput;
        private bool jumpInput;
        private bool grapInput;
        private bool dashInput;

        // 碰撞相关 - 由CollisionSenses负责，此类型为CoreComponent
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isTouchingLedge;
        protected bool isTouchingCelling;

        protected virtual Movement Movement => Movement ?? core.GetCoreComponent<Movement>();

        protected CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        public override PlayerStateType PlayerStateType => PlayerStateType.Grounded;

        public PlayerGroundedState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

            if (CollisionSenses)    // 将碰撞检测解耦出去了
            {
                isGrounded = CollisionSenses.Ground;
                isTouchingWall = CollisionSenses.WallFront;
                isTouchingLedge = CollisionSenses.LedgeHorizontal;
                isTouchingCelling = CollisionSenses.Ceiling;
            }
        }

        public override void Enter()
        {
            base.Enter();

            //playerStateMachine.playerStats[PlayerStateType.Jump];
            // playerStateMachine.playerStats[PlayerStateType.Dash];
        }

        public override void Exit() => base.Exit();

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;
            grapInput = player.InputHandler.GrapInput;
            dashInput = player.InputHandler.DashInput;

            if (player.InputHandler.AttackInputs[((int)CombatInputs.Primary)] && !isTouchingCelling)
                stateMachine.ChangeState(PlayerStateType.PrimaryAttack);
            if (player.InputHandler.AttackInputs[((int)CombatInputs.Secondary)] && !isTouchingCelling)
                stateMachine.ChangeState(PlayerStateType.SecondaryAttack);

        }

        public override void PhysicsUpdate()=> base.PhysicsUpdate();
    }
}
