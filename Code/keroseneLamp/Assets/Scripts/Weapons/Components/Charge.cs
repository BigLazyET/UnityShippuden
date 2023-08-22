using Assets.Scripts.Common;
using Assets.Scripts.CoreSystem;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Charge : WeaponComponent<ChargeData, AttackCharge>
    {
        private int currentCharge;

        private TimeNotifier timeNotifier;

        private ParticleManager particleManager;

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            currentCharge = currentAttackData.InitialChargeAmount;
            timeNotifier.Init(new TimeNotifierData { duration = currentAttackData.ChargeTime });
        }

        protected override void HandleOnExit()
        {
            base.HandleOnExit();

            timeNotifier.Disable();
        }

        private void HandleNotify()
        {
            currentCharge++;

            if (currentCharge > currentAttackData.NumberOfCharges)
            {
                currentCharge = currentAttackData.NumberOfCharges;
                timeNotifier.Disable();

                particleManager.StartParticlesRelative(currentAttackData.FullyIncreaseIndicatorParticlePrefab,
                    currentAttackData.ParticleOffset, Quaternion.identity);
            }
            else
                particleManager.StartParticlesRelative(currentAttackData.ChargeIncreaseIndicatorParticlePrefab,
                    currentAttackData.ParticleOffset, Quaternion.identity);
        }

        public int TakeFinalChargeReading()
        {
            timeNotifier.Disable();
            return currentCharge;
        }

        #region Lifecycle
        protected override void Awake()
        {
            base.Awake();

            timeNotifier = new TimeNotifier();
            timeNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();

            particleManager = weapon.Core.GetCoreComponent<ParticleManager>();
        }

        private void Update()
        {
            timeNotifier.Tick();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            timeNotifier.OnNotify -= HandleNotify;
        }
        #endregion
    }
}
