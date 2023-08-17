using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    /// <summary>
    /// When The GameObject it attached to is disabled or destroyed, it fires event OnDisableEvent
    /// Why need this
    /// When projectile stuck in stone or something else, the stone is broken and destroyed, the projectfile will be stucked in air
    /// it is weired, so we need notify the projectile to do some logic/physics behaviors, like dropped to ground, and so on...
    /// </summary>
    public class OnDisableNotifier : MonoBehaviour
    {
        public event Action OnDisabled;

        private void OnDestroy()
        {
            OnDisabled?.Invoke();
        }

        [ContextMenu("Test")]
        private void Test()
        {
            gameObject.SetActive(false);
        }
    }
}
