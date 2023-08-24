using System;

namespace Assets.Scripts.EnemySystem
{
    public class Enemy1 : EnemyEntity
    {
        private E1_ChargeState chargeState;

        public override void Awake()
        {
            base.Awake();

            chargeState = new E1_ChargeState(this, MobDataSO, StateMachine, "charge");

            BodyStatus.Health.OnCurrentValueZero += HandleCurrentValueZero;
        }

        private void OnDestroy()
        {
            BodyStatus.Health.OnCurrentValueZero -= HandleCurrentValueZero;
        }

        private void HandleCurrentValueZero()
        {
            throw new NotImplementedException();
        }
    }
}
