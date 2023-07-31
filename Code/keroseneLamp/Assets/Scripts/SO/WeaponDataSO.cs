using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        // 在运行时可以修改Animator，可以用作技能动画的切换等
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }

        [SerializeField] public int NumberOfAttacks { get; private set; }

        public List<ComponentData> ComponentDatas { get; private set; }

        public T GetComponentData<T>() => ComponentDatas.OfType<T>().FirstOrDefault();

        public List<Type> GetAllDependencies() => ComponentDatas.Select(x=>x.ComponentDependency).ToList();

        public void AddComponentData(ComponentData data)
        {
            if (ComponentDatas.Any(c => c.GetType() == data.GetType()))
                return;
            ComponentDatas.Add(data);
        }
    }
}
