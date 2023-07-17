using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 巡逻成员
    /// </summary>
    public class PatrolState : FsmState
    {
        Transform[] paths;  // 巡逻路径(巡逻点)
        int index = 0;
        Transform target;

        public PatrolState(int stateId, FsmStateSystem fsmSystem, Transform target) : base(stateId, fsmSystem)
        {
            this.paths = GameObject.Find("Path").GetComponentsInChildren<Transform>();
            this.target = target;
        }

        public override void OnEnter(params object[] args)
        {
            base.OnEnter(args);

            Debug.Log("Enter Patrol State");
        }

        public override void OnExit(params object[] args)
        {
            base.OnExit(args);

            Debug.Log("Exit Patrol State");
        }

        public override void OnStay(params object[] args)
        {
            if(args == null || args.Length == 0)
            {
                Debug.LogError("PatrolState args length is zero");
                return;
            }

            var npc = args[0] as GameObject;
            npc.transform.LookAt(paths[index].position);
            npc.transform.Translate(Vector3.forward * Time.deltaTime * 3);
            if(Vector3.Distance(npc.transform.position, paths[index].position) < 1)
            {
                index++;
                index = index % paths.Length;
            }

            if(Vector3.Distance(npc.transform.position, target.transform.position) < 5)
            {
                fsmSystem.TranslateState((int)NpcState.Chase);
            }
        }
    }
}
