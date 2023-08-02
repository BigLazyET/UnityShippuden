using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLedgeClimbState : PlayerState
    {
        private Vector2 detectedPos;

        public override PlayerStateType PlayerStateType => PlayerStateType.LedgeClimb;

        public PlayerLedgeClimbState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    }
}
