/*using System.Collections;
using UnityEngine;




public class StateManager
{
    private IState currentState;

    public void SetInitialState(IState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }

    public void TransitionToState(IState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }

}
*/