using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class CoreComponent : MonoBehaviour
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
                Debug.LogError("No core on the parent!");

            core.AddCoreComponent(this);
        }

        public virtual void LogicUpdate() { }
    }
}
