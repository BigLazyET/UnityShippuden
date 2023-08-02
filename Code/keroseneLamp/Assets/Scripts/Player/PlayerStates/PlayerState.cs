using Assets.Scripts.SO;
using CoreNs = Assets.Scripts.Core; 

namespace Assets.Scripts.Player
{
    public abstract class PlayerState
    {
        public abstract PlayerStateType PlayerStateType { get; }

        protected CoreNs.Core core;

        protected Player player;
        protected PlayerDataSO playerDataSO;
        protected PlayerStateMachine playerStateMachine;

        protected bool isAnimationFinished;
        protected bool isExitingState;

        protected float startTime;

        private string animBoolName;

        public PlayerState(Player player, PlayerDataSO playerDataSO, PlayerStateMachine playerStateMachine, string animBoolName)
        {
            this.core = player.Core;
            this.animBoolName = animBoolName;
            this.player = player;
            this.playerDataSO = playerDataSO;
            this.playerStateMachine = playerStateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void DoChecks() { }

        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}
