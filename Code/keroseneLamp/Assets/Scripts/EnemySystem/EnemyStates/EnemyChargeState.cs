using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyChargeState : EnemyState<ChargeData>
    {
        private Movement Movement => Movement ?? core.GetCoreComponent<Movement>();
        private CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        protected bool isPlayerInMinAgroRange;
        protected bool isDetectingLedge;
        protected bool isDetectingWall;
        protected bool isChargeTimeOver;
        protected bool performCloseRangeAction;

        public EnemyChargeState(EnemyEntity enemyEntity, MobDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }
}
