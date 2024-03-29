/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

// Idle State
public class WorkerIdleState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;

    public WorkerIdleState(StateManager stateMachine, SmartOne smartOnes)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(WorkerMovementState.idle);

        // Enter logic for Idle state
        Debug.Log("Entering Worker Idle state");
    }

    public void Update()
    {
        if (smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag, out GameObject closestEnemy))
        {
            if(closestEnemy.CompareTag("Tree") || closestEnemy.CompareTag("Gold Mine"))
            {
                stateMachine.TransitionToState(new WorkerGoState(stateMachine, smartOnes, closestEnemy));
            }
        }
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting Worker Idle state");
    }

}

public class WorkerGoState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;

    public WorkerGoState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(WorkerMovementState.run);

        // Enter logic for Idle state
        Debug.Log("Entering WorkerWorkState state");
        UpdateDirection();
    }

    public void Update()
    {

        if (smartOnes.ShouldInRadius(smartOnes.doRadius, smartOnes.Tag, out GameObject closestEnemy))
        {
            if (closestEnemy.CompareTag("Tree") )
            {
                stateMachine.TransitionToState(new WorkerChopState(stateMachine, smartOnes, closestEnemy));
            }
            else if(closestEnemy.CompareTag("Gold Mine"))
            {
                stateMachine.TransitionToState(new WorkerBuildState(stateMachine, smartOnes, closestEnemy));
            }
        }
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting WorkerWorkState state");
    }
    private void UpdateDirection()
    {
        if ((closestEnemy.transform.position.x < smartOnes.transform.position.x && smartOnes.isFaceRight)
            || (closestEnemy.transform.position.x > smartOnes.transform.position.x && !smartOnes.isFaceRight))
        {
            smartOnes.Flip();
        }
        smartOnes.Agent.SetDestination(new Vector3(closestEnemy.transform.position.x , closestEnemy.transform.position.y+0.5f, closestEnemy.transform.position.z));
    }
}

public class WorkerChopState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;

    public WorkerChopState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(WorkerMovementState.chop);

        // Enter logic for Idle state
        Debug.Log("Entering WorkerWorkState state");
    }

    public void Update()
    {
        return;
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting WorkerWorkState state");
    }

}

public class WorkerBuildState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;

    public WorkerBuildState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(WorkerMovementState.build);

        // Enter logic for Idle state
        Debug.Log("Entering WorkerWorkState state");
    }

    public void Update()
    {

        return;
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting WorkerWorkState state");
    }

}*/