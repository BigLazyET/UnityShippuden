using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 不知道你是处理玩家跳跃的判断条件的？反正就我而言，射线或者子物体检测地面图层：如果角色在地面上，则允许跳跃；反之则不允许。
    // 但是这样在游玩的时候会导致一个问题：当你想要【连跳】时，单按跳跃键，你以为自己已经落到了地面，而实际上，你还在空中，从而造成了“按键失灵”的问题。这对于玩家的游玩体验有着相当大的影响。
    // 而解决这个问题的方法，就是允许指令的预输入，在预输入后的一段时间内，若检测到条件满足，再执行操作——即“输入缓冲”。
    [SerializeField] float jumpInputBufferTime = 0.5f;  // 输入缓冲时间，指令的预输入 => 宽判定，用户还未真正完全落地的时候就按下跳跃键，这时候也进行跳跃
    PlayerInputActions playerInputActions;  // 配置Input Action后生成的对应类
    WaitForSeconds waitJumpInputBufferTime;   // 输入缓冲时间，指令的预输入

    Vector2 axes => playerInputActions.Gameplay.Axes.ReadValue<Vector2>();  // 轴输入
   
    public bool Jump => playerInputActions.Gameplay.Jump.WasPressedThisFrame();  // 在这一帧，如果按下跳跃动作绑定的【任何】按键，那么这个属性值将会为true
    
    public bool StopJump => playerInputActions.Gameplay.Jump.WasReleasedThisFrame();    // 判断松开跳跃按键

    public float AxisX => axes.x;   // X方向上的轴输入的值

    public bool Move => AxisX != 0f;    // X轴方向不为零时，代表玩家在运动(不管是否在地面上)

    // 是否启用输入缓冲特性，考虑哪些地方会用到这个字段？
    // 1. 用户跳跃的时候，启用并设定启用时间
    // 2. 因为此特性适用于【单按跳跃键】+【连跳】的情况下，所以在着陆的时候(还没有到达地面)，当特性启用着+按下跳跃键，就执行跳跃操作和动画
    public bool HasJumpInputBuffer;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    private void OnEnable()
    {
        playerInputActions.Gameplay.Jump.canceled += Jump_canceled;
    }

    // Unity调试的一种手段，非常直观
    // void OnGUI()
    // {
    //     Rect rect = new Rect(200, 200, 200, 200);
    //     string message = "Has Jump Input Buffer: " + HasJumpInputBuffer;
    //     GUIStyle style = new GUIStyle();

    //     style.fontSize = 20;
    //     style.fontStyle = FontStyle.Bold;

    //     GUI.Label(rect, message, style);
    // }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HasJumpInputBuffer = false;
    }

    public void EnableGameplayInputs()
    {
        playerInputActions.Gameplay.Enable();   // 启用在Input Action中配置的Gameplay动作表
        Cursor.lockState = CursorLockMode.Locked;   // 项目中不会用到鼠标点击的话，可以将光标设定为锁定模式
    }

    public void DisableGameplayInputs()
    {
        playerInputActions?.Gameplay.Disable(); // 禁用Input Action中配置的Gameplay动作表
    }

    /// <summary>
    /// 输入缓冲的计时器
    /// 我们不能无休止的启用输入缓冲，想象一下，当用户从很高的地方下落，在下落的初始就又按下了跳跃键
    /// 在不存在二段跳或者二段跳及多段跳机会已用光的前提下，如果依然记录下用户按下了跳跃键的这个操作那就会出现很奇怪的问题和现象
    /// </summary>
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    private IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        HasJumpInputBuffer = false;
    }
}
