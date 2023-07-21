using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

namespace Assets.Scripts.Interactive
{
    // 这边可以继承针对NPC的Input Actions集合
    // 毕竟交互一般是共通的操作，比如对话，只需要按键左右即可等等
    public class NpcInteractiveBase : Interactive, INpcInteractiveActions
    {
        public void OnDialogue(InputAction.CallbackContext context)
        {
            isInput = context.started;
        }

        public override void NormalActive()
        {
            Debug.Log("NormalActive");
        }

        public override void SpecificActive()
        {
            Debug.Log("SpecificActive");
        }
    }
}
