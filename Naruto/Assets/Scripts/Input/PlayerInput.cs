using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ��֪�����Ǵ��������Ծ���ж������ģ��������Ҷ��ԣ����߻��������������ͼ�㣺�����ɫ�ڵ����ϣ���������Ծ����֮������
    // ���������������ʱ��ᵼ��һ�����⣺������Ҫ��������ʱ��������Ծ��������Ϊ�Լ��Ѿ��䵽�˵��棬��ʵ���ϣ��㻹�ڿ��У��Ӷ�����ˡ�����ʧ�顱�����⡣�������ҵ��������������൱���Ӱ�졣
    // ������������ķ�������������ָ���Ԥ���룬��Ԥ������һ��ʱ���ڣ�����⵽�������㣬��ִ�в��������������뻺�塱��
    [SerializeField] float jumpInputBufferTime = 0.5f;  // ���뻺��ʱ�䣬ָ���Ԥ���� => ���ж����û���δ������ȫ��ص�ʱ��Ͱ�����Ծ������ʱ��Ҳ������Ծ
    PlayerInputActions playerInputActions;  // ����Input Action�����ɵĶ�Ӧ��
    WaitForSeconds waitJumpInputBufferTime;   // ���뻺��ʱ�䣬ָ���Ԥ����

    Vector2 axes => playerInputActions.Gameplay.Axes.ReadValue<Vector2>();  // ������
   
    public bool Jump => playerInputActions.Gameplay.Jump.WasPressedThisFrame();  // ����һ֡�����������Ծ�����󶨵ġ��κΡ���������ô�������ֵ����Ϊtrue
    
    public bool StopJump => playerInputActions.Gameplay.Jump.WasReleasedThisFrame();    // �ж��ɿ���Ծ����

    public float AxisX => axes.x;   // X�����ϵ��������ֵ

    public bool Move => AxisX != 0f;    // X�᷽��Ϊ��ʱ������������˶�(�����Ƿ��ڵ�����)

    // �Ƿ��������뻺�����ԣ�������Щ�ط����õ�����ֶΣ�
    // 1. �û���Ծ��ʱ�����ò��趨����ʱ��
    // 2. ��Ϊ�����������ڡ�������Ծ����+��������������£���������½��ʱ��(��û�е������)��������������+������Ծ������ִ����Ծ�����Ͷ���
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

    // Unity���Ե�һ���ֶΣ��ǳ�ֱ��
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
        playerInputActions.Gameplay.Enable();   // ������Input Action�����õ�Gameplay������
        Cursor.lockState = CursorLockMode.Locked;   // ��Ŀ�в����õ�������Ļ������Խ�����趨Ϊ����ģʽ
    }

    public void DisableGameplayInputs()
    {
        playerInputActions?.Gameplay.Disable(); // ����Input Action�����õ�Gameplay������
    }

    /// <summary>
    /// ���뻺��ļ�ʱ��
    /// ���ǲ�������ֹ���������뻺�壬����һ�£����û��Ӻܸߵĵط����䣬������ĳ�ʼ���ְ�������Ծ��
    /// �ڲ����ڶ��������߶�������������������ù��ǰ���£������Ȼ��¼���û���������Ծ������������Ǿͻ���ֺ���ֵ����������
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
