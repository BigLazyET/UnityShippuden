using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // PlayerState是ScriptableObject类型的，且特定PlayerState打上CreateAssetMenu标签，让其称为asset资源
    // 至此可以在Assets/Data中右键 -> 按照CreateAssetMenu标签menuName路径创建fileName资源
    // 最后就可以把其拖到PlayerStateMachine的Inspector中的这个states字段中
    [SerializeField] PlayerState[] states;

    Animator animator;

    PlayerInput input;

    PlayerController player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        input = GetComponent<PlayerInput>();

        player = GetComponent<PlayerController>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach (var state in states)
        {
            state.Initialize(input, animator, player, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        SwitchOne(stateTable[typeof(PlayerState_Idle)]);
    }
}
