using Assets.Scripts.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public override PlayerStateType PlayerStateType => PlayerStateType.Move;

        public PlayerMoveState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }
    }
}
