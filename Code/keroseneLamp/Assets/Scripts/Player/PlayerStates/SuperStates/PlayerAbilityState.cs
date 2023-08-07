using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// 所有玩家能力的状态的基状态
    /// 玩家能力: Jump, Dash
    /// </summary>
    public class PlayerAbilityState : PlayerState
    {
        private bool isGrounded;

        protected bool isAbilityDone;

        protected virtual Movement Movement => Movement ?? core.GetCoreComponent<Movement>();

        public CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        public override PlayerStateType PlayerStateType => PlayerStateType.Ability;

        public PlayerAbilityState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

            if (CollisionSenses)
                isGrounded = CollisionSenses.Ground;
        }

        public override void Enter()
        {
            base.Enter();

            isAbilityDone = false;
        }

        public override void Exit() => base.Exit();

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(isAbilityDone)
            {
                if (isGrounded && Movement.CurrentVelocity.y < 0.01f)
                    stateMachine.ChangeState(PlayerStateType.Idle);
                else
                    stateMachine.ChangeState(PlayerStateType.InAir);
            }
        }

        public override void PhysicsUpdate() => base.PhysicsUpdate();
    }
}
