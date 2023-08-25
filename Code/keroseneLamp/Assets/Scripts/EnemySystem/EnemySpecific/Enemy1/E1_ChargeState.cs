using Assets.Scripts.SO;

namespace Assets.Scripts.EnemySystem
{
    public class E1_ChargeState : EnemyChargeState
    {
        public E1_ChargeState(EnemyEntity enemyEntity, EnemyDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}
