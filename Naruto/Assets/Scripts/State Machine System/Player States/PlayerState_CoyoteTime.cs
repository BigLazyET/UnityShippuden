using UnityEngine;

/// <summary>
/// 我们把土狼时间也当成一种状态来处理
/// 土狼时间主要是在那段时间内消除重力
/// </summary>
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float coyoteTime = 0.1f;

    public override void Enter()
    {
        base.Enter();

        player.SetUseGravity(false);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetUseGravity(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }

        if (StateDuration > coyoteTime || !input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.Move(runSpeed);
    }
}
