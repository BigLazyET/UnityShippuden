using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Float", fileName = "PlayerState_Float")]
public class PlayerState_Float : PlayerState
{
    [SerializeField] VoidEventChannel playerDefeatedEventChannel;
    [SerializeField] float floatingSpeed = 0.5f;
    [SerializeField] Vector3 floatingPositionOffset;
    [SerializeField] Vector3 vfxOffset;
    [SerializeField] ParticleSystem vfx;

    Vector3 floatingPosition;
    Transform PlayerTransform => player.transform;

    public override void Enter()
    {
        base.Enter();

        playerDefeatedEventChannel.Broadcast();

        var vfxPosition = PlayerTransform.position + new Vector3(PlayerTransform.localScale.x * vfxOffset.x, vfxOffset.y);
        Instantiate(vfx, vfxPosition, Quaternion.identity);

        floatingPosition = PlayerTransform.position + floatingPositionOffset;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Vector3.Distance(PlayerTransform.position, floatingPosition) > floatingSpeed * Time.deltaTime)
        {
            PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, floatingPosition, floatingSpeed * Time.deltaTime);
        }
        else
        {
            floatingPosition = (Vector3)Random.insideUnitCircle;
        }
    }
}
