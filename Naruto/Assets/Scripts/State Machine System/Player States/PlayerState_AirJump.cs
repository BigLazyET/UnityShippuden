using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/AirJump",fileName ="PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] ParticleSystem jumpVFX;
    [SerializeField] AudioClip jumpSFX;

    public override void Enter()
    {
        base.Enter();

        input.HasJumpInputBuffer = false;
        player.SetVelocityY(jumpForce);
        player.VoicePlayer.PlayOneShot(jumpSFX);
        Instantiate(jumpVFX, player.transform.position, Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ��ʱ��������Ծ�Ĵ�С��
        // ����Ҫ�ȵ���ɫ��������Ȼ��y���ٶ�Ϊ������ʱ���ִ������|���䶯��
        // ���û�ȡ����Ծ��ʱ��Ҳ��ʱֹͣ��Ծ�Ӷ���ʼ����
        if (input.StopJump || player.isFalling)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.Move(moveSpeed); // �ڿ�����Ȼ�����ƶ�
    }
}
