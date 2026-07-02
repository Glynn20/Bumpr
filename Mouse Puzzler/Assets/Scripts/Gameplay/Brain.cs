using System;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Brain<TStateMachine, EState, TContext> : MonoBehaviour
    where EState : Enum
    where TStateMachine : StateMachine<EState, TContext>
{
    [SerializeField] protected TStateMachine _stateMachine;
    protected TContext _context;

    protected virtual void Start()
    {
        if (_stateMachine == null) 
        {
            Debug.LogWarning("Brain is missing StateMachine.", this);
            return;
        }
        _context = InitializeContext();
        _stateMachine?.SetUp(_context);
    }

    protected abstract TContext InitializeContext();

}
