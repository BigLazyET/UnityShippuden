using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// Charge Coponent 和 ProjectileSpawner Component 之间的桥梁
    /// </summary>
    public class ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
    {
        private Charge charge;
        private ProjectileSpawner projectileSpawner;

        private bool hasReadCharge;

        private ChargeProjectileSpawnerStrategy strategy = new ChargeProjectileSpawnerStrategy();

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            hasReadCharge = false;
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasReadCharge) return;

            strategy.AngleVaration = currentAttackData.AngleVariation;
            strategy.ChargeAmount = charge.TakeFinalChargeReading();

            projectileSpawner.SetProjectileSpawnerStrategy(strategy);

            hasReadCharge = true;
        }

        #region Lifecycle
        protected override void Start()
        {
            base.Start();

            charge = GetComponent<Charge>();
            projectileSpawner = GetComponent<ProjectileSpawner>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        }
        #endregion
    }
}
