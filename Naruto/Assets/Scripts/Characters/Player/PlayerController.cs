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

    public bool CanAirJump { get; set; }    // �Ƿ���Կ�����Ծ������������Ծ��ǰ���ǳԵ���ʯ

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

        GetComponent<StateMachine>().SwitchState(typeof(PlayerState_Defeated)); // ����defeat����
    }

    private void Start()
    {
        input.EnableGameplayInputs();   // ����Input Action�е�Gameplay������
    }

    public void Move(float speed)
    {
        if (input.Move)
        {
            transform.localScale = new Vector3(input.AxisX, 1f, 1f);    // �����������
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
    /// ����������ʱ��
    /// ��ʹ���������Ӷ�����ҿ������뿪ƽ̨���������ʱ���ڣ����Լ�����Ծ
    /// </summary>
    /// <param name="value"></param>
    public void SetUseGravity(bool value)
    {
        rigidBody.useGravity = value;
    }
}
