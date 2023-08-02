using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateMachine
    {
        public IDictionary<PlayerStateType, PlayerState> playerStats = new Dictionary<PlayerStateType, PlayerState>();

        public PlayerState CurrentState { get; private set; }

        public void AddState(PlayerState state)
        {
            if (state == null)
            {
                Debug.LogError("FsmSystem AddState is not null");
                return;
            }

            if (CurrentState == null)
            {
                CurrentState = state;
                return;
            }

            if (playerStats.ContainsKey(state.PlayerStateType))
            {
                Debug.LogError($"FsmSystem map already contains stateId:{state.PlayerStateType}");
                return;
            }

            //states[state.StateId] = state;
            playerStats.Add(state.PlayerStateType, state);
        }

        public void RemoveState(PlayerStateType playerStateType)
        {
            if (!playerStats.ContainsKey(playerStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{playerStateType}");
                return;
            }

            playerStats.Remove(playerStateType);
        }

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerStateType playerStateType)
        {
            if (CurrentState == null)
            {
                Debug.LogError("Current FsmState is null");
                return;
            }

            if (!playerStats.ContainsKey(playerStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{playerStateType}");
                return;
            }

            var newState = playerStats[playerStateType];

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
