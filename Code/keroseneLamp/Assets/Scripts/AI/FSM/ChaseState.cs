using UnityEngine;

namespace AI.FSM
{
    public class ChaseState : FsmState
    {
        Transform target;

        public ChaseState(int stateId, FsmStateSystem fsmSystem, Transform target) : base(stateId, fsmSystem)
        {
            this.target = target;
        }

        public override void OnEnter(params object[] args)
        {
            base.OnEnter(args);

            Debug.Log("Enter Chase State");
        }

        public override void OnExit(params object[] args)
        {
            base.OnExit(args);

            Debug.Log("Exit Chase State");
        }

        public override void OnStay(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Debug.LogError("ChaseState args length is zero");
                return;
            }

            var npc = args[0] as GameObject;

            if (Vector3.Distance(npc.transform.position, target.position) > 5)
            {
                fsmSystem.TranslateState((int)NpcState.Patrol);
            }

            npc.transform.LookAt(target.position);
            npc.transform.Translate(Vector3.forward * Time.deltaTime * 5, Space.Self);
        }
    }
}
