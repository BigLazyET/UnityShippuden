using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public override PlayerStateType PlayerStateType => PlayerStateType.Idle;

        public PlayerIdleState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }
    }
}
