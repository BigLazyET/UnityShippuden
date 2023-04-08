using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬��
/// ��֤��ǰֻ��һ��״̬��ִ�У�״̬���߼���������������״̬����֡���·�����ִ��
/// ״̬����Ϊ����ִ��Update��FixedUpdate�Ķ��󣻶�״̬��ֻ��Ϊ��Դ�ṩ
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
