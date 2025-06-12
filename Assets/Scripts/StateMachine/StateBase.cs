using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class StateBase : MonoBehaviour
{
    private StateMachine _owningStateMachine;
    private Animator _anim;
    private Transform target;
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Transition();
    public abstract void UpdateState();

    public virtual void InitState(StateMachine parent)
    {
        _owningStateMachine = parent;
        _anim = GetComponent<Animator>();
        target = PlayerStats.PlayerStatsInstance.transform;
    }
}
