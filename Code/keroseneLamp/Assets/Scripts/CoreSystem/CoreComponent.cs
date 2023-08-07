using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
                Debug.LogError("No core on the parent!");

            core.AddCoreComponent(this);
        }

        public virtual void LogicUpdate() { }
    }
}
