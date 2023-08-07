using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public abstract class PlayerState
    {
        private string animBoolName;
        
        protected Core core;
        protected Player player;
        protected PlayerDataSO playerDataSO;
        protected PlayerStateMachine stateMachine;

        protected bool isAnimationFinished;
        protected bool isExitingState;
        protected float startTime;

        public abstract PlayerStateType PlayerStateType { get; }

        public PlayerState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName)
        {
            this.core = player.Core;
            this.animBoolName = animBoolName;
            this.player = player;
            this.playerDataSO = playerDataSO;
            this.stateMachine = playerStateMachine;
        }

        public virtual void Enter()
        {
            DoChecks();

            player.Animator.SetBool(animBoolName, true);

            startTime = Time.time;
            isExitingState = false;
            isAnimationFinished = false;
        }

        public virtual void Exit() 
        {
            player.Animator.SetBool(animBoolName, false);
            isExitingState = true;
        }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() => DoChecks();

        /// <summary>
        /// 用在Enter 和 PhysicsUpdate 中
        /// </summary>
        public virtual void DoChecks() { }

        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}
