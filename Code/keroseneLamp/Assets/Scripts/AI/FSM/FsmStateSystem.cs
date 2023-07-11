using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 状态机
    /// 存储对象所有的状态映射以及添加、删除、切换、保持状态方法等
    /// </summary>
    public class FsmStateSystem
    {
        public Dictionary<int, FsmState> states = new Dictionary<int, FsmState>();

        private FsmState currentState;

        public void AddState(FsmState state)
        {
            if(state == null)
            {
                Debug.LogError("FsmSystem AddState is not null");
                return;
            }

            if(currentState == null)
            {
                currentState = state;
                return;
            }

            if(states.ContainsKey(state.StateId))
            {
                Debug.LogError($"FsmSystem map already contains stateId:{state.StateId}");
                return;
            }

            //states[state.StateId] = state;
            states.Add(state.StateId, state);
        }

        public void RemoveState(int stateId)
        {
            if(!states.ContainsKey(stateId))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{stateId}");
                return;
            }

            states.Remove(stateId);
        }

        public void Update(GameObject npc)
        {
            if(currentState == null) { return; }
            currentState.OnStay(npc);
        }

        public void TranslateState(int stateId)
        {
            if (currentState == null)
            {
                Debug.LogError("Current FsmState is null");
                return;
            }

            if (states.ContainsKey(stateId))
            {
                Debug.LogError($"FsmSystem map already contains stateId:{stateId}");
                return;
            }

            var state = states[stateId];

            currentState.OnExit(this);
            currentState = state;
            currentState.OnEnter(this);
        }
    }
}
