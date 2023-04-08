using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Fall", fileName ="PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    // 动画过渡函数(曲线)，可以在Inspector面板中自行调整
    // 相当于选中Animator面板中两个State之间的Transition线选中之后，在Inspector面板中调整的动画之间切换，重叠时间等一样
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float moveSpeed = 5f;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.IsGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Land));
        }

        if (input.Jump)
        {
            if (player.CanAirJump)
            {
                stateMachine.SwitchState(typeof(PlayerState_AirJump));
                return;
            }

            // 注意此时是在【下落状态】下，按下的跳跃键，为了防止吞键，缓存用户按下跳跃键的这个操作
            // 当用户真正落地，但是并不是落地那一刻按下的跳跃键，而是在落地之前一小会按下的跳跃键 => 那我们已经缓存了这个键的操作，所以直接判定让角色可以跳跃
            // 不至于判定的那么严格，让用户感觉吞键：可能真的角色落地了用户并没有那一刻按跳跃键，而是在角色快要落地的时候按下的跳跃键，这个时候宽松判定让用户获得更好的游戏体验
            input.SetJumpInputBufferTimer();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.Move(moveSpeed);
        player.SetVelocityY(speedCurve.Evaluate(StateDuration));
    }
}
