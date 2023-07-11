namespace AI.FSM
{
    /// <summary>
    /// 状态机基类
    /// </summary>
    public abstract class FsmState
    {
        protected int stateId;
        protected FsmStateSystem fsmSystem;

        public int StateId { get => stateId; }

        public FsmState(int stateId, FsmStateSystem fsmSystem)
        {
            this.stateId = stateId;
            this.fsmSystem = fsmSystem;
        }

        public virtual void OnEnter(params object[] args) { }
        public virtual void OnStay(params object[] args) { }
        public virtual void OnExit(params object[] args) { }

    }
}
