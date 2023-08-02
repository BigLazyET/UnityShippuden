using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    public class PlayerInAirState : PlayerState
    {
        

        public override PlayerStateType PlayerStateType => PlayerStateType.InAir;

        public PlayerInAirState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }
    }
}
