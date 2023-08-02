using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Core
{
    /// <summary>
    /// 在某些模块中，比如武器模块等，Core代表模块中所有Entity的根
    /// </summary>
    public class Core : MonoBehaviour
    {
        [field: SerializeField] public GameObject Root { get; private set; }

        private readonly IList<CoreComponent> CoreComponents = new List<CoreComponent>();

        private void Awake()
        {
            Root = Root ? Root : transform.parent.gameObject;
        }

        public void LogicUpdate()
        {
            foreach (var coreComponent in CoreComponents)
            {
                coreComponent.LogicUpdate();
            }
        }

        public void AddCoreComponent(CoreComponent coreComponent)
        {
            if (CoreComponents.Contains(coreComponent)) return;
            CoreComponents.Add(coreComponent);
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var coreComponent = CoreComponents.OfType<T>().FirstOrDefault();
            if (coreComponent)
                return coreComponent;

            coreComponent = GetComponentInChildren<T>();

            if (coreComponent)
                return coreComponent;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent => value = GetCoreComponent<T>();
    }
}
