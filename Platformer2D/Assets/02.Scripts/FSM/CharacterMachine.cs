using UnityEngine;
using System.Collections.Generic;

public enum State
{
    None,
    Idle,
    Move,
    Jump,
    Fall,
    Land,
}

public class CharacterMachine : MonoBehaviour
{
    public State current;
    private Dictionary<State, IWorkflow<State>> _states;

    public void Initialize(IEnumerable<KeyValuePair<State, IWorkflow<State>>> copy)
    {
        _states = new Dictionary<State, IWorkflow<State>>(copy);
    }

    public bool ChangeState(State newState)
    {
        if (newState == current)
            return false;

        current = newState;
        return true;
    }

    private void Start()
    {
        Initialize(CharacterStateWorkflows.GetWorkflowsForPlayer(this));
    }

    private void Update()
    {
        ChangeState(_states[current].MoveNext());
    }
}