using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] VoidEventChannel levelClearedEventChannel;

    PlayerInput input;
    Rigidbody rigidBody;
    PlayerGroundDetector groundDetector;

    public bool IsGrounded => groundDetector.IsGrounded;

    public AudioSource VoicePlayer { get; private set; }

    public bool Victory { get; set; }

    public bool CanAirJump { get; set; }    // 是否可以空中跳跃：本作空中跳跃的前提是吃到宝石

    public bool isFalling => rigidBody.velocity.y < 0f && !IsGrounded;

    public float MoveSpeed => Mathf.Abs(rigidBody.velocity.x);

    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();
        VoicePlayer = GetComponentInChildren<AudioSource>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
    }

    private void OnEnable()
    {
        levelClearedEventChannel.AddListener(OnLevelCleared);
    }

    private void OnDisable()
    {
        levelClearedEventChannel.RemoveListener(OnLevelCleared);
    }

    private void OnLevelCleared()
    {
        Victory = true;
    }

    public void OnDefeated()
    {
        input.DisableGameplayInputs();

        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;
        rigidBody.detectCollisions = false;

        GetComponent<StateMachine>().SwitchState(typeof(PlayerState_Defeated)); // 播放defeat动画
    }

    private void Start()
    {
        input.EnableGameplayInputs();   // 启用Input Action中的Gameplay动作表
    }

    public void Move(float speed)
    {
        if (input.Move)
        {
            transform.localScale = new Vector3(input.AxisX, 1f, 1f);    // 解决朝向问题
        }

        SetVelocityX(speed * input.AxisX);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    /// <summary>
    /// 适用于土狼时间
    /// 不使用重力，从而让玩家可以在离开平台的这段土狼时间内，可以继续跳跃
    /// </summary>
    /// <param name="value"></param>
    public void SetUseGravity(bool value)
    {
        rigidBody.useGravity = value;
    }
}
