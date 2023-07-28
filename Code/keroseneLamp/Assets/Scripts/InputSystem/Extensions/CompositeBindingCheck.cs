using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.InputSystem.ReBinding
{
    public class CompositeBindingCheck : MonoBehaviour
    {
        InputActions inputActions;

        private void Start()
        {
            inputActions = new InputActions();
            Debug.Log($"Left Stick：{inputActions.Player.Move.bindings[0].isComposite}");
            Debug.Log($"WASD：{inputActions.Player.Move.bindings[1].isComposite}");

            //Load:
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
                inputActions.LoadBindingOverridesFromJson(rebinds);

            //Save:
            rebinds = inputActions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
        }
    }
}
