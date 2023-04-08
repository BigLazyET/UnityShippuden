using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Defeated", fileName = "PlayerState_Defeated")]
public class PlayerState_Defeated : PlayerState
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] AudioClip[] voice;

    public override void Enter()
    {
        base.Enter();

        Instantiate(vfx, player.transform.position, Quaternion.identity);
        player.VoicePlayer.PlayOneShot(voice[Random.Range(0, voice.Length)]);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Float));
        }
    }
}
