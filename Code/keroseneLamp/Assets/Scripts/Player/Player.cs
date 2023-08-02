using Assets.Scripts.SO;
using UnityEngine;
using CoreNs = Assets.Scripts.Core;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        #region Components
        public CoreNs.Core Core { get; private set; }

        public Animator Animator { get; private set; }

        public PlayerInputHandler InputHandler { get; private set; }
        #endregion

        public PlayerStateMachine StateMachine { get; private set; }

        [field: SerializeField] private PlayerDataSO playerDataSO;

        private void Awake()
        {
            Core = GetComponentInChildren<CoreNs.Core>();

            StateMachine = new PlayerStateMachine();
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            
        }
    }
}
