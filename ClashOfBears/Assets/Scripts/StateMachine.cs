using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private State<T> currentState;

    public void SetState(State<T> newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

}

public abstract class State<T>
{
    protected T _Owner;
    protected State(T owner)
    {
        _Owner = owner;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}