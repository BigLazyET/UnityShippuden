using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

namespace Assets.Scripts.Interactive
{
    // 这边可以继承针对NPC的Input Actions集合
    // 毕竟交互一般是共通的操作，比如对话，只需要按键，然后下一句下一句即可等等
    public class NpcInteractiveBase : Interactive, INpcInteractiveActions
    {
        public void OnDialogue(InputAction.CallbackContext context)
        {
            isInput = context.started;

            if(isNeedInput && isInput && isNeedTouch && isTouch)
            {
                if (SpecialActiveCheck())
                    Debug.Log("special action");
                else
                    Debug.Log("normal action");
            }
        }
    }
}
