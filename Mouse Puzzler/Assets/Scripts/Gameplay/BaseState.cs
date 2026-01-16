using System;
using UnityEngine;

public abstract class BaseState<EState, TContext> where EState : Enum 
{
    protected TContext Context;
    public BaseState(EState key, TContext context)
    {
        StateKey = key;
        Context = context;
    }

    public EState StateKey { get; private set; }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void UpdateState();
    public abstract EState GetNextState();
}
