using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateMachine
    {
        public IDictionary<PlayerStateType, PlayerState> playerStates = new Dictionary<PlayerStateType, PlayerState>();

        public PlayerState CurrentState { get; private set; }

        public void AddState(PlayerState state)
        {
            if (state == null)
            {
                Debug.LogError("FsmSystem AddState is not null");
                return;
            }

            if (playerStates.ContainsKey(state.PlayerStateType))
            {
                Debug.LogError($"FsmSystem map already contains stateId:{state.PlayerStateType}");
                return;
            }

            //states[state.StateId] = state;
            playerStates.Add(state.PlayerStateType, state);
        }

        public void RemoveState(PlayerStateType playerStateType)
        {
            if (!playerStates.ContainsKey(playerStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{playerStateType}");
                return;
            }

            playerStates.Remove(playerStateType);
        }

        public void Initialize(PlayerStateType playerStateType)
        {
            CurrentState = playerStates[playerStateType];
            CurrentState.Enter();
        }

        public void ChangeState(PlayerStateType playerStateType)
        {
            if (CurrentState == null)
            {
                Debug.LogError("Current FsmState is null");
                return;
            }

            if (!playerStates.ContainsKey(playerStateType))
            {
                Debug.LogError($"FsmSystem map not contains stateId:{playerStateType}");
                return;
            }

            var newState = playerStates[playerStateType];

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public T GetState<T>() where T : PlayerState => playerStates.Values.OfType<T>().FirstOrDefault() ?? default(T);
    }
}
