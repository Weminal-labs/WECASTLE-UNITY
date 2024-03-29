/*using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public interface IState
{
    void Enter(SmartOne smartOnes);
    void Execute(SmartOne smartOnes);
    void Exit(SmartOne smartOnes);
}

public enum WarriorMovementState
{
    idle,
    run,
    attackSide,
    attackDown,
    attackUp,
    hurt,
    death
};
public enum ArcherMovementState
{
    idle,               //0
    run,                //1
    attackSide,         //2
    attackDown,         //3
    attackDiagonalDown,  //4
    attackUp,           //5
    attackDiagonalUp,    //6
    hurt,               //7
    death               //8
};
public enum WorkerMovementState
{
    idle,       //0 
    run,        //1
    idleCarry,  //2
    runCarry,   //3 
    build,      //4
    chop,       //5
    hurt,       //6
    death       //7
};

public class SmartOne : MonoBehaviour
{

    [SerializeField]
    public float detectionRadius;
    [SerializeField]
    public float doRadius;
    [SerializeField]
    public string[] Tag;

    private IState currentState;

    Animator animator;

    public NavMeshAgent Agent;

    public bool isFaceRight;

    public GameObject arrowPrefap;
    public bool IsMovingToUserTarget { get; private set; } // Flag to indicate user-initiated movement


    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        currentState = new IdleState(this);


        isFaceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute(this);
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, doRadius);

    }

    public bool ShouldInRadius(in float radius, string[] tags, out Transform closestEnemy)
    {
        closestEnemy = null;
        float closestDistance = float.MaxValue;

        // Example: Check if there are enemies within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var collider in colliders)
        {
            foreach (var tag in tags)
            {
                if (collider.CompareTag(tag))
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);

                    // Check if the current enemy is closer than the previously found closest enemy
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = collider.GameObject().transform;
                    }
                }
            }


        }

        return closestEnemy != null;
    }

    public void PlayAnimation(WarriorMovementState animation)
    {

        animator.SetInteger("state", (int)animation);
    }
    public void PlayAnimation(WorkerMovementState animation)
    {

        animator.SetInteger("state", (int)animation);
    }
    public void PlayAnimation(ArcherMovementState animation)
    {

        animator.SetInteger("state", (int)animation);
    }
    public void PlayAnimation(int animation)
    {

        animator.SetInteger("state", animation);
    }

    public bool HasAnimationCompleted()
    {
        // Check if the attack animation has completed
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // Animation has completed
            return true;
        }

        // Animation is still playing
        return false;
    }

    public bool HasAnimationReach(float time)
    {
        // Check if the attack animation has completed
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
        {
            // Animation has completed
            return true;
        }

        // Animation is still playing
        return false;
    }

    public void Flip()
    {

        // Get the current local scale of the object
        Vector3 currentScale = transform.localScale;

        // Invert the X component of the scale to flip horizontally
        currentScale.x *= -1;

        // Apply the new local scale to the object
        transform.localScale = currentScale;
        isFaceRight = !isFaceRight;
    }
    public void Spawn()
    {

    }
}
*/