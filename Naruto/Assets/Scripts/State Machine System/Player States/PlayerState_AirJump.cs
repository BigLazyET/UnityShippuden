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

        // 及时反馈：跳跃的大小跳
        // 不需要等到角色到达最后点然后y轴速度为负数的时候才执行下落|下落动画
        // 当用户取消跳跃的时候，也及时停止跳跃从而开始下落
        if (input.StopJump || player.isFalling)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        player.Move(moveSpeed); // 在空中依然可以移动
    }
}
