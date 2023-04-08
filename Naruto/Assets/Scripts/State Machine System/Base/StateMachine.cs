using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机
/// 保证当前只有一种状态在执行；状态的逻辑和物理方法都会在状态机的帧更新方法中执行
/// 状态机作为真正执行Update和FixedUpdate的对象；而状态都只作为资源提供
/// </summary>
public class StateMachine : MonoBehaviour
{
    private IState currentState;
    protected Dictionary<Type, IState> stateTable;

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    public void SwitchOne(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchOne(Type newStateType)
    {
        var state = stateTable[newStateType];
        SwitchOne(state);
    }

    public void SwitchState(IState newState)
    {
        currentState.Exit();
        //currentState = newState;
        //currentState.Enter();
        SwitchOne(newState);
    }

    public void SwitchState(Type newStateType)
    {
        var state = stateTable[newStateType];
        SwitchState(state);
    }
}
