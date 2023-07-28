using System;
using UnityEngine.InputSystem;
using static InputActions;

namespace Assets.Scripts.Interactive
{
    /// <summary>
    /// 道具物体
    /// 涉及拾取，装备，卸载，丢弃，买入，卖出等交互
    /// </summary>
    public class ItemInteractiveBase : Interactive, INpcInteractiveActions
    {
        public void OnDialogue(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}
