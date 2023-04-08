using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Fall", fileName ="PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    // �������ɺ���(����)��������Inspector��������е���
    // �൱��ѡ��Animator���������State֮���Transition��ѡ��֮����Inspector����е����Ķ���֮���л����ص�ʱ���һ��
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

            // ע���ʱ���ڡ�����״̬���£����µ���Ծ����Ϊ�˷�ֹ�̼��������û�������Ծ�����������
            // ���û�������أ����ǲ����������һ�̰��µ���Ծ�������������֮ǰһС�ᰴ�µ���Ծ�� => �������Ѿ�������������Ĳ���������ֱ���ж��ý�ɫ������Ծ
            // �������ж�����ô�ϸ����û��о��̼���������Ľ�ɫ������û���û����һ�̰���Ծ���������ڽ�ɫ��Ҫ��ص�ʱ���µ���Ծ�������ʱ������ж����û���ø��õ���Ϸ����
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
