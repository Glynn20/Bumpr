using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState, TContext> : MonoBehaviour 
    where EState : Enum 
{
    // Store all States
    protected Dictionary<EState, BaseState<EState, TContext>> _states 
        = new Dictionary<EState, BaseState<EState, TContext>>(); 

    protected BaseState<EState, TContext> _currentState;
    protected TContext _context;
    protected bool isTransitioningState = false;

    [SerializeField] protected EState initialKey;

    protected void Update()
    {
        if (isTransitioningState) return;

        EState nextStateKey = _currentState.GetNextState();

        if (nextStateKey.Equals(_currentState.StateKey)) { _currentState.UpdateState(); }
        else { TransitionToState(nextStateKey); }
    }

    protected abstract void SetUpStates();
    protected abstract BaseState<EState, TContext> SetInitialState();

    public void SetUp(TContext context)
    {
        _context = context;
        SetUpStates();
        _currentState = SetInitialState();

        _currentState.Enter();
    }
    public void TransitionToState(EState key)
    {
        isTransitioningState = true;
        _currentState.Exit();
        _currentState = _states[key];
        _currentState.Enter();
        isTransitioningState = false;
    }
}
