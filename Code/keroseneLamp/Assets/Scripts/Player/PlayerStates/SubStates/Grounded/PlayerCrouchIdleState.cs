using Assets.Scripts.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public override PlayerStateType PlayerStateType => PlayerStateType.CrouchIdle;

        public PlayerCrouchIdleState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();

            Movement.SetVelocityZero();
            player.
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
