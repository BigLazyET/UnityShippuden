using System;
using System.Reflection;
using Unity.VisualScripting;
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

    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        // 如果每次攻击的组件数据都相同，则为 true，从而避免必须设置重复数据的问题
        [SerializeField] private bool repeatData;

        [SerializeField] private T[] attackDatas;

        public T GetAttackData(int index) => attackDatas[repeatData ? 0 : index];

        public T[] GetAllAttackData() => attackDatas;

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (int i = 0; i < attackDatas.Length; i++)
            {
                attackDatas[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttack)
        {
            base.InitializeAttackData(numberOfAttack);

            var newAttacks = repeatData ? 1 : numberOfAttack;
            var oldAttacks = attackDatas == null ? 0 : attackDatas.Length;

            if (newAttacks == oldAttacks) return;

            Array.Resize(ref attackDatas, newAttacks);

            if (oldAttacks < newAttacks)
            {
                for (int i = oldAttacks; i < newAttacks - oldAttacks; i++)
                {
                    var newAttackData = Activator.CreateInstance(typeof(T));
                    attackDatas[i] = newAttackData as T;
                }
            }

            SetAttackDataNames();
        }
    }
}
