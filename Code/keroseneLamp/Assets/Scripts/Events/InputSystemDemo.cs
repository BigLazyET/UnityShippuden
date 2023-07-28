using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Events
{
    /// <summary>
    /// 参考链接：https://blog.csdn.net/weixin_61427881/article/details/130556928
    /// </summary>
    public class InputSystemDemo : MonoBehaviour
    {
        public DeviceType deviceType;

        // 第二种两个InputAction直接通过代码创建
        public InputAction moveAction;
        public InputAction jumpAction;

        // 第四种：用Input Action Asset生成C#代码获取输入
        public InputActions inputActions;

        private void Start()
        {
            // 第二种：代码创建Custom-InputAction来获取输入
            // 增加2D Vector:WASD绑定
            moveAction.AddCompositeBinding("2DVector(mode=2)")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            // 增加单个按键：Space绑定
            jumpAction.AddBinding("<Keyboard>/space");
            // 需要手动启用Action
            moveAction.Enable();
            // 需要手动启用Action
            jumpAction.Enable();

            // 第四种：用Input Action Asset生成C#代码获取输入
            // 创建一个实例
            inputActions = new InputActions();
            // 启用Player分组里的Actions
            inputActions.Player.Enable();
            inputActions.Player.Fire.performed += Fire_performed;
            // 还有一种方式是，将当前类直接继承 InputActions.IPlayerActions 接口
            // 然后实现其中的OnFire，OnLook，OnJump，OnMove
            // 然后给设置下回调是自己即可：inputActions.Player.SetCallbacks(this);
        }

        private void Fire_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("fire the hole");
        }

        private void Update()
        {
            // 第一种：直接从设备对应类中获取输入
            if(deviceType == DeviceType.Gamepad)
            {
                var gamePad = Gamepad.current;  // 获取当前Gamepad
                var direction = gamePad.leftStick.value;  // 获取左摇杆方向
                transform.Translate(direction * Time.deltaTime);
            }
            else if(deviceType == DeviceType.Keyboard)
            {
                var keyboard = Keyboard.current;    // 获取当前Keyboard
                if(keyboard.spaceKey.isPressed)
                    GetComponent<Rigidbody>().velocity = Vector3.up * Time.deltaTime;
            }
            else
            {
                var mouse = Mouse.current;
                if(mouse.leftButton.isPressed)
                    GetComponent<Rigidbody>().velocity = Vector3.up * Time.deltaTime;
            }

            // 第二种：代码创建Custom-InputAction来获取输入
            //获取输入
            var dir = moveAction.ReadValue<Vector2>();
            //移动
            transform.Translate(new Vector3(dir.x, dir.y, 0) * Time.deltaTime);
            //跳跃
            if (jumpAction.ReadValue<float>() >= 1)
                GetComponent<Rigidbody>().AddForce(Vector3.up * 10f);
        }

        public enum DeviceType
        {
            Gamepad,
            Keyboard,
            Mouse,
        }
    }
}
