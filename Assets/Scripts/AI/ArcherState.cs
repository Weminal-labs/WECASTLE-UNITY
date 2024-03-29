/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// Idle State

public class ArcherIdleState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;

    public ArcherIdleState(StateManager stateMachine, SmartOne smartOnes)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
    }

    public void Enter()
    {
        smartOnes.Agent.stoppingDistance = 0;

        smartOnes.PlayAnimation(ArcherMovementState.idle);

        // Enter logic for Idle state
        Debug.Log("Entering Idle state");
    }

    public void Update()
    {
        Debug.Log("Updating Archer Idle state");

        if (smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag, out GameObject closestEnemy))
        {
            Debug.Log("Transitioning to Chase state");

            stateMachine.TransitionToState(new ArcherChaseState(stateMachine, smartOnes, closestEnemy));
        }
    }

    public void Exit()
    {
        // Exit logic for Idle state
        Debug.Log("Exiting Idle state");
    }

}


// Chase State
public class ArcherChaseState : IState
{
    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;

    public ArcherChaseState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.Agent.stoppingDistance = 0;

        smartOnes.PlayAnimation(ArcherMovementState.run);

        // Enter logic for Chase state
        Debug.Log("Entering Chase state");

        UpdateDirection(); // Update the direction on entering the state
    }

    public void Update()
    {

        // Update logic for Chase state
        if (smartOnes.ShouldInRadius(smartOnes.doRadius, smartOnes.Tag, out GameObject closestEnemy))
        {
            stateMachine.TransitionToState(new ArcherAttackState(stateMachine, smartOnes, closestEnemy));
        }
        else if (!smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag, out GameObject newClosestEnemy))
        {
            Debug.Log("Transitioning to Idle state");

            stateMachine.TransitionToState(new ArcherIdleState(stateMachine, smartOnes));

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
public class ArcherAttackState : IState
{
    public float attackAngleThreshold = 22.5f; // Threshold angle for attacking


    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;
    private Vector3 direction;
    private float angle;
    public float arrowSpeed = 10f;
    public float arrowLifetime = 3f;
    public ArcherAttackState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.Agent.stoppingDistance = 4;

        direction = closestEnemy.transform.position - smartOnes.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine the direction to attack based on the angle
        if (Mathf.Abs(angle) < attackAngleThreshold 
            || angle > 180 - attackAngleThreshold 
            || angle < -180 + attackAngleThreshold)
        {
            smartOnes.PlayAnimation(ArcherMovementState.attackSide);

            // Attack to the right
                SpawnArrow(direction, angle);
        }
        else if (angle > 45 - attackAngleThreshold && angle < 45 + attackAngleThreshold 
            || angle > 135 - attackAngleThreshold && angle < 135 + attackAngleThreshold )
        {
            // Attack diagonally up
            smartOnes.PlayAnimation(ArcherMovementState.attackDiagonalUp);
                SpawnArrow(direction, angle);

        }
        else if (angle > 90 - attackAngleThreshold && angle < 90 + attackAngleThreshold)
        {
            // Attack upwards
            smartOnes.PlayAnimation(ArcherMovementState.attackUp);
                SpawnArrow(direction, angle);

        }
        else if (angle < -45 + attackAngleThreshold && angle > -45 - attackAngleThreshold
            || angle < -135 + attackAngleThreshold && angle > -135 - attackAngleThreshold )
        {
            // Attack diagonally down
            smartOnes.PlayAnimation(ArcherMovementState.attackDiagonalDown);
                SpawnArrow(direction, angle);

        }
        else if (angle < -90 + attackAngleThreshold && angle > -90 - attackAngleThreshold)
        {
            // Attack downwards
            smartOnes.PlayAnimation(ArcherMovementState.attackDown);
                SpawnArrow(direction, angle);

        }
        // Enter logic for Attack state
        Debug.Log("Entering Attack state");
    }

    public void Update()
    {
        // Update logic for Attack state
        if (smartOnes.HasAnimationCompleted())
        {
            // Check if there's still an enemy in attack radius
            if (smartOnes.ShouldInRadius(smartOnes.detectionRadius, smartOnes.Tag, out GameObject newClosestEnemy))
            {
                // Transition to Chase state with the new closest enemy
                stateMachine.TransitionToState(new ArcherChaseState(stateMachine, smartOnes, newClosestEnemy));
            }
            else
            {
                // No enemy in attack radius, transition to Idle state
                stateMachine.TransitionToState(new ArcherIdleState(stateMachine, smartOnes));
            }
        }
    }

    public void Exit()
    {
        // Exit logic for Attack state
        Debug.Log("Exiting Attack state");
        //smartOnes.Agent.isStopped = false;

    }

    public void SpawnArrow(Vector3 direction, float angle)
    {

        // Spawn arrow at archer's position
        GameObject arrow = GameObject.Instantiate(smartOnes.arrowPrefap, smartOnes.transform.position, Quaternion.identity);

        // Rotate the arrow to face the enemy
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Move the arrow in the direction of the enemy (optional)
        arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;
        GameObject.Destroy(arrow, arrowLifetime);

    }
}

// Attack State
public class ArcherWWaitingState : IState
{


    private readonly StateManager stateMachine;
    private readonly SmartOne smartOnes;
    private readonly GameObject closestEnemy;

    public ArcherWWaitingState(StateManager stateMachine, SmartOne smartOnes, GameObject closestEnemy)
    {
        this.stateMachine = stateMachine;
        this.smartOnes = smartOnes;
        this.closestEnemy = closestEnemy;
    }

    public void Enter()
    {
        smartOnes.PlayAnimation(ArcherMovementState.idle);

    }

    public void Update()
    {
        if(smartOnes.HasAnimationCompleted())
            stateMachine.TransitionToState(new ArcherAttackState(stateMachine, smartOnes,closestEnemy));
    }

    public void Exit()
    {
        // Exit logic for Attack state
        Debug.Log("Exiting Waiting state");
        //smartOnes.Agent.isStopped = false;

    }

}

*/