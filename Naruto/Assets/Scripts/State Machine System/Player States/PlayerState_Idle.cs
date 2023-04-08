using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Idle",fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField] float deceleration = 5f;   // Move����

    public override void Enter()
    {
        base.Enter();

        currentSpeed = player.MoveSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
        if (input.Jump)
        {
            stateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        if (!player.IsGrounded) // �ذ�ͻȻ��ʧ������
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.SetVelocityX(currentSpeed * player.transform.localScale.x);
    }
}
