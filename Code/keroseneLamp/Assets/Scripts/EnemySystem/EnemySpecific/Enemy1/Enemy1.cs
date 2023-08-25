using System;

namespace Assets.Scripts.EnemySystem
{
    public class Enemy1 : EnemyEntity
    {
        public override void Awake()
        {
            base.Awake();

            StateMachine.AddState(new E1_ChargeState(this, MobDataSO, StateMachine, "charge"));

            PoiseDamageReceiver.Poison.OnCurrentValueZero += HandleCurrentValueZero;
        }

        private void OnDestroy()
        {
            PoiseDamageReceiver.Poison.OnCurrentValueZero -= HandleCurrentValueZero;
        }

        private void HandleCurrentValueZero()
        {
            //StateMachine.ChangeState()
        }
    }
}
