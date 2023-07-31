using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }
        
        public void SetComponentName() => name = GetType().Name;

        protected abstract void SetComponentDependency();

        public virtual void SetAttackDataNames() { }

        public virtual void InitializeAttackData(int numberOfAttack) { }
    }
}
