using UnityEngine;

public abstract class PlayerState : ScriptableObject, IState
{
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

    private int stateHash;  // ����״̬Ψһid
    private float stateStartTime;   // ״̬��ʼʱ��

    protected PlayerInput input;
    protected Animator animator;
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;
    protected float currentSpeed;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;    // ��ǰ֡�ӿ�ʼ�����ڵ�ʱ�� - ״̬(����)��ʼʱ��

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
    /// �߼����·���
    /// ״̬��Ϊʲô��������߼����·�����ֻ��Ҫ���Ǳ�д�ӡ���ǰ״̬������ת��������Щ״̬���Ĵ��뼴��
    /// (�������޷����ܵ�״ֱ̬���л�������״̬�ĵ���һ�������������ܵ�״̬������д����������߼��ж�û���κ�����)
    /// ͬʱ��������˵����״̬��������һ���ص㣺�����ԭ�������߼����з�֧��д��Update�����е����Σ�����ά�� + ����״̬֮����߼��������ж�if�ǳ���
    /// �൱��Unity������Animator������趨����״̬֮���Transition�ߣ�=> ֻ�������ǲ���Ҫͨ��UI�����ķ�ʽ������ͨ��״̬������ķ�ʽ��ʵ�֣�
    /// </summary>
    public virtual void LogicUpdate() { }

    /// <summary>
    /// ������·���
    /// ע���˵��ͬ��
    /// </summary>
    public virtual void PhysicUpdate() { }  
}
