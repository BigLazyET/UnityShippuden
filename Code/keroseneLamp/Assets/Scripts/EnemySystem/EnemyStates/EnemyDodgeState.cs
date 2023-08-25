using Assets.Scripts.SO;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyDodgeState : EnemyState<DodgeData>
    {
        public override EnemyStateType EnemyStateType => EnemyStateType.Dodge;

        public EnemyDodgeState(EnemyEntity enemyEntity, EnemyDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }
    }
}
