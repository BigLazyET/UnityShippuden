using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 巡逻成员
    /// </summary>
    public class PatrolState : FsmState
    {
        Transform[] paths;  // 巡逻路径(巡逻点)

        public PatrolState(int stateId, FsmStateSystem fsmSystem) : base(stateId, fsmSystem)
        {
        }
    }
}
