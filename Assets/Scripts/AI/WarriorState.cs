using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

// Idle State
public class IdleState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOnes smartOnes;

    public IdleState(StateManager stateMachine, SmartOnes smartOnes)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(MovementState.idle);

        // Enter logic for Idle state
        Debug.Log("Entering Idle state");
    }

    public void Update()
    {
        Debug.Log("Updating Idle state");

        if (smartOnes.ShouldInRadius(smartOnes.detectionRadius,smartOnes.Tag,out GameObject closestEnemy))
        {
            Debug.Log("Transitioning to Chase state");

            stateMachine.TransitionToState(new ChaseState(stateMachine,smartOnes, closestEnemy));
        }
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting Idle state");
    }

}


// Chase State
public class ChaseState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOnes smartOnes;
    private readonly GameObject closestEnemy;

    public ChaseState(StateManager stateMachine,SmartOnes smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(MovementState.run);

        // Enter logic for Chase state
        Debug.Log("Entering Chase state");

        UpdateDirection(); // Update the direction on entering the state
    }

    public void Update()
    {

        // Update logic for Chase state
        if (smartOnes.ShouldInRadius(smartOnes.doRadius, smartOnes.Tag ,out GameObject closestEnemy))
        {
            stateMachine.TransitionToState(new AttackState(stateMachine, smartOnes,closestEnemy));
        }
        else if (!smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag , out GameObject newClosestEnemy))
        {
            Debug.Log("Transitioning to Idle state");

            stateMachine.TransitionToState(new IdleState(stateMachine, smartOnes));

        }
    }

    public void Exit()
    {
        // Exit logic for Chase state
        Debug.Log("Exiting Chase state");
        //smartOnes.Agent.isStopped = true;
    }

    private void UpdateDirection()
    {
        if ((closestEnemy.transform.position.x < smartOnes.transform.position.x && smartOnes.isFaceRight) 
            || (closestEnemy.transform.position.x > smartOnes.transform.position.x && !smartOnes.isFaceRight))
        {
            smartOnes.Flip();
        }
        smartOnes.Agent.SetDestination(new Vector3(closestEnemy.transform.position.x, closestEnemy.transform.position.y, closestEnemy.transform.position.z));
    }
}

// Attack State
public class AttackState : IState
{
    public float attackAngleThreshold = 45f; // Threshold angle for attacking

    private readonly StateManager stateMachine;
    private readonly SmartOnes smartOnes;
    private readonly GameObject closestEnemy;
    public AttackState(StateManager stateMachine,SmartOnes smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        Vector3 direction = closestEnemy.transform.position - smartOnes.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine the direction to attack based on the angle
        if (Mathf.Abs(angle) < attackAngleThreshold)
        {
            // Attack to the right
            smartOnes.PlayAnimation(MovementState.attackSide);
        }
        else if (angle > 90 - attackAngleThreshold && angle < 90 + attackAngleThreshold)
        {
            // Attack upwards
            smartOnes.PlayAnimation(MovementState.attackUp);
        }
        else if (angle > 180 - attackAngleThreshold || angle < -180 + attackAngleThreshold)
        {
            // Attack to the left
            smartOnes.PlayAnimation(MovementState.attackSide);
        }
        else if (angle < -90 + attackAngleThreshold && angle > -90 - attackAngleThreshold)
        {
            // Attack downwards
            smartOnes.PlayAnimation(MovementState.attackDown);
        }
        // Enter logic for Attack state
            Debug.Log("Entering Attack state");
    }

    public void Update()
    {
        // Update logic for Attack state
        if (smartOnes.HasAttackAnimationCompleted())
        {
            // Check if there's still an enemy in attack radius
            if (smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag, out GameObject newClosestEnemy))
            {
                // Transition to Chase state with the new closest enemy
                stateMachine.TransitionToState(new ChaseState(stateMachine, smartOnes, newClosestEnemy));
            }
            else
            {
                // No enemy in attack radius, transition to Idle state
                stateMachine.TransitionToState(new IdleState(stateMachine, smartOnes));
            }
        }
    }

    public void Exit()
    {
        // Exit logic for Attack state
        Debug.Log("Exiting Attack state");
        //smartOnes.Agent.isStopped = false;

    }

}

// Die State
public class DieState : IState
{
    private readonly StateManager stateMachine;

    public DieState(StateManager stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        // Enter logic for Die state
        Debug.Log("Entering Die state");
    }

    public void Update()
    {
        // Update logic for Die state (may not be needed)
    }

    public void Exit()
    {
        // Exit logic for Die state
        Debug.Log("Exiting Die state");
    }
}

