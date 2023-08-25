using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyChargeState : EnemyState<ChargeData>
    {
        private Movement Movement => Movement ?? core.GetCoreComponent<Movement>();
        private CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        public override EnemyStateType EnemyStateType => EnemyStateType.Charge;

        protected bool isPlayerInMinAgroRange;
        protected bool isDetectingLedge;
        protected bool isDetectingWall;
        protected bool performCloseRangeAction;

        protected bool isChargeTimeOver;

        public EnemyChargeState(EnemyEntity enemyEntity, EnemyDataSO enemyDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, enemyDataSO, stateMachine, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

            isPlayerInMinAgroRange = enemyEntity.PlayerCheckSense.CheckPlayerInMinAgroRange();
            performCloseRangeAction = enemyEntity.PlayerCheckSense.CheckPlayerInCloseRangeAction();
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.WallFront;
        }

        public override void Enter()
        {
            base.Enter();

            isChargeTimeOver = false;
            Movement?.SetVelocityX(stateData.ChargeSpeed * Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Movement?.SetVelocityX(stateData.ChargeSpeed * Movement.FacingDirection);
            if (Time.time >= StartTime + stateData.ChargeTime)
                isChargeTimeOver = true;
        }
    }
}
