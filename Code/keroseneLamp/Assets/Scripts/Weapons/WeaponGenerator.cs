using Assets.Scripts.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 根据Weapon Data进行相应Component的绑定
    /// </summary>
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponDataSO weaponDataSO;

        private Animator animator;
        private IList<WeaponComponent> componentsAlraeayOnWeapon = new List<WeaponComponent>();
        private IList<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private IList<Type> componentDependencies = new List<Type>();

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();

            GenerateWeapon(weaponDataSO);
        }

        private void GenerateWeapon(WeaponDataSO weaponDataSO)
        {
            weapon.SetData(weaponDataSO);

            componentDependencies.Clear();
            componentsAddedToWeapon.Clear();
            componentsAlraeayOnWeapon.Clear();

            componentsAlraeayOnWeapon = GetComponents<WeaponComponent>();
            componentDependencies = weaponDataSO.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                if (componentsAddedToWeapon.Any(x => x.GetType() == dependency))
                    continue;

                var weaponComponent = componentsAlraeayOnWeapon.FirstOrDefault(x => x.GetType() == dependency);
                if(weaponComponent == null)
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                weaponComponent.Init();

                componentsAddedToWeapon.Add(weaponComponent);
            }

            var waitToDel = componentsAlraeayOnWeapon.Except(componentsAddedToWeapon);
            foreach (var component in waitToDel)
                Destroy(component);

            animator.runtimeAnimatorController = weaponDataSO.AnimatorController;
        }
    }
}
