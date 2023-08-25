using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyDeathState : EnemyState<DeathData>
    {
        public override EnemyStateType EnemyStateType => EnemyStateType.Death;

        public EnemyDeathState(EnemyEntity enemyEntity, EnemyDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            GameObject.Instantiate(stateData.DeathBloodParticle, enemyEntity.transform.position, stateData.DeathBloodParticle.transform.rotation);
            GameObject.Instantiate(stateData.DeathChunckParticle, enemyEntity.transform.position, stateData.DeathChunckParticle.transform.rotation);

            enemyEntity.gameObject.SetActive(false);
        }
    }
}
