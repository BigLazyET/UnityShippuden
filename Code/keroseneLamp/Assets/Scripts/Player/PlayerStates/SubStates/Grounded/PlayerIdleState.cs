using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public override PlayerStateType PlayerStateType => PlayerStateType.Idle;

        public PlayerIdleState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            Movement.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExitingState)
            {
                if (xInput != 0)
                    stateMachine.ChangeState(PlayerStateType.Move);
                if(yInput == -1)
                    stateMachine.ChangeState(PlayerStateType.CrouchIdle);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
