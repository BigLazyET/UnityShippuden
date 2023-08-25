using Assets.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    public class EnemyStateMachine
    {
        public IDictionary<EnemyStateType, EnemyState> enemyStates = new Dictionary<EnemyStateType, EnemyState>();

        public EnemyState CurrentState { get; private set; }

        public void AddState(EnemyState state)
        {
            if (state == null)
            {
                Debug.LogError("FsmSystem AddState is not null");
                return;
            }

            if (enemyStates.ContainsKey(state.EnemyStateType))
            {
                Debug.LogError($"FsmSystem map already contains stateId:{state.EnemyStateType}");
                return;
            }

            //states[state.StateId] = state;
            enemyStates.Add(state.EnemyStateType, state);
        }

        public void RemoveState(EnemyStateType enemyStateType)
        {
            if (!enemyStates.ContainsKey(enemyStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{enemyStateType}");
                return;
            }

            enemyStates.Remove(enemyStateType);
        }

        public void Initialize(EnemyStateType enemyStateType)
        {
            CurrentState = enemyStates[enemyStateType];
            CurrentState.Enter();
        }

        public void ChangeState(EnemyStateType enemyStateType)
        {
            if (CurrentState == null)
            {
                Debug.LogError("Current FsmState is null");
                return;
            }

            if (!enemyStates.ContainsKey(enemyStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{enemyStateType}");
                return;
            }

            var newState = enemyStates[enemyStateType];

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public T GetState<T>() where T : PlayerState => enemyStates.Values.OfType<T>().FirstOrDefault() ?? default(T);
    }
}
