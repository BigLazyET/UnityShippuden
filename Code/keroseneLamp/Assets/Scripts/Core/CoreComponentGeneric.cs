using UnityEngine;

namespace Assets.Scripts.Core
{
    public class CoreComponentGeneric<T>  where T : CoreComponent
    {
        private Core core;
        private T coreComponent;

        public T CoreComponent => coreComponent ? coreComponent : core.GetCoreComponent<T>();

        public CoreComponentGeneric(Core core)
        {
            if (core == null)
                Debug.LogWarning($"Core is null for component: {typeof(T)}");

            this.core = core;
        }
    }
}
