using Assets.Scripts.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.EnemySystem
{
    public class E1_ChargeState : EnemyChargeState
    {
        public E1_ChargeState(EnemyEntity enemyEntity, MobDataSO mobDataSO, EnemyStateMachine stateMachine, string animBoolName) : base(enemyEntity, mobDataSO, stateMachine, animBoolName)
        {
        }
    }
}
