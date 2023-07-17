using UnityEngine;

namespace AI.FSM
{
    public class Enemy : MonoBehaviour
    {
        private FsmStateSystem fsmSystem;
        public Transform target;

        private void Start()
        {
            fsmSystem = new FsmStateSystem();

            var patrolState = new PatrolState((int)NpcState.Patrol, fsmSystem, target);
            var chaseState = new ChaseState((int)NpcState.Chase, fsmSystem, target);

            fsmSystem.AddState(patrolState);
            fsmSystem.AddState(chaseState);
        }

        private void Update()
        {
            fsmSystem.Update(this.gameObject);
        }
    }
}
