using Assets.Scripts.SO;

namespace Assets.Scripts.Player
{
    public class PlayerWallJumpState : PlayerTouchingWallState
    {
        private int wallJumpDirection;

        public override PlayerStateType PlayerStateType => PlayerStateType.WallJump;

        public PlayerWallJumpState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerDataSO, playerStateMachine, animBoolName)
        {
        }

        public void DetermineWallJumpDirection()
        {
            if (isTouchingWall)
                wallJumpDirection = -Movement.FacingDirection;
            else
                wallJumpDirection = Movement.FacingDirection;
        }
    }
}
