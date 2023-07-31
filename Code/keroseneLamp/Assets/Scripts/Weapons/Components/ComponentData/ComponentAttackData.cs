using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class ComponentAttackData<T> : ComponentData where T : AttackData
    {
        // 如果每次攻击的组件数据都相同，则为 true，从而避免必须设置重复数据的问题
        [SerializeField] private bool repeatData;

        [SerializeField] private T[] attackDatas;

        public T GetData(int index) => attackDatas[repeatData ? 0 : index];

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

            if(oldAttacks < newAttacks)
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
