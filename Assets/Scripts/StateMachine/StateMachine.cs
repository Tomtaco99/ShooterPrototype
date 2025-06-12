using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StateMachine : MonoBehaviour
{
    [SerializeField] public SenseBase[] senses;
    private StateBase currentState;
    [SerializeField] private StateBase initialState;
    [SerializeField] private StateBase[] allStates;
    
    public void Awake()
    {
        currentState = initialState;
        currentState.Enter();
        allStates = GetComponents<StateBase>();
        foreach (StateBase state in allStates)
        {
            state.InitState(this);
        }
    }

    public void ChangeState(StateBase nextState)
    {
        if (nextState == currentState) return;
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.UpdateState();
        currentState.Transition();
    }

}
