using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyState
    {
        protected MobDataSO mobDataSO;
        protected EnemyEntity enemyEntity;
        protected EnemyStateMachine stateMachine;
        protected string animBoolName;
        protected Core core;

        public float StartTime { get; protected set; }

        public EnemyState(EnemyEntity enemyEntity, MobDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName)
        {
            this.enemyEntity = enemyEntity;
            this.mobDataSO = mobDataSO;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;

            core = enemyEntity.Core;
        }

        public virtual void Enter() 
        {
            StartTime = Time.time;

            enemyEntity.Animator.SetBool(animBoolName, true);

            DoChecks();
        }

        public virtual void Exit() => enemyEntity.Animator.SetBool(animBoolName, true);

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() => DoChecks();

        public virtual void DoChecks() { }
    }

    public abstract class EnemyState<T> : EnemyState where T : EnemyComponentData
    {
        protected T data;

        protected EnemyState(EnemyEntity enemyEntity, MobDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            data = mobDataSO.GetData<T>();

            base.Enter();
        }
    }
}
