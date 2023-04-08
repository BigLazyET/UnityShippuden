using UnityEngine;

public abstract class PlayerState : ScriptableObject, IState
{
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

    private int stateHash;  // 动画状态唯一id
    private float stateStartTime;   // 状态开始时间

    protected PlayerInput input;
    protected Animator animator;
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;
    protected float currentSpeed;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;    // 当前帧从开始到现在的时间 - 状态(动画)开始时间

    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public void Initialize(PlayerInput input, Animator animator, PlayerController player, PlayerStateMachine stateMachine)
    {
        this.input = input;
        this.animator = animator;
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() 
    {
        animator.CrossFade(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    /// <summary>
    /// 逻辑更新方法
    /// 状态机为什么做到解耦：逻辑更新方法中只需要考虑编写从【当前状态】可以转化到【哪些状态】的代码即可
    /// (就像你无法从跑的状态直接切换到下落状态的道理一样，所以你在跑的状态方法中写关于下落的逻辑判定没有任何意义)
    /// 同时上述例子说明了状态机的另外一个特点：解放了原本所以逻辑所有分支都写在Update方法中的尴尬：难于维护 + 各个状态之间的逻辑以来和判断if非常多
    /// 相当于Unity引擎中Animator面板中设定各个状态之间的Transition线！=> 只不过我们不需要通过UI拖拉的方式，而是通过状态机编码的方式来实现！
    /// </summary>
    public virtual void LogicUpdate() { }

    /// <summary>
    /// 物理更新方法
    /// 注意和说明同上
    /// </summary>
    public virtual void PhysicUpdate() { }  
}
