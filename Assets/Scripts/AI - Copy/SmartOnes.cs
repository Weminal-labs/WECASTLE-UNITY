using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MovementState { idle, run, attackSide,  attackDown, attackUp, hurt, death };

public class SmartOnes : MonoBehaviour
{

    [SerializeField]
    public float detectionRadius;
    [SerializeField] 
    public float attackRadius;
    [SerializeField]
    public string enemyTag;

    Animator animator;

    public StateManager stateMachine;

    public NavMeshAgent Agent;

    public bool isFaceRight;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        stateMachine = new StateManager();
        stateMachine.SetInitialState(new IdleState(stateMachine,this));
        isFaceRight = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        stateMachine.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

    public bool ShouldInRadius(in float radius,out Transform closestEnemy)
    {
        closestEnemy = null;
        float closestDistance = float.MaxValue;

        // Example: Check if there are enemies within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                // Check if the current enemy is closer than the previously found closest enemy
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy != null;
    }

    public void PlayAnimation(MovementState animation)
    {

        animator.SetInteger("state", (int)animation);
    }
    public bool HasAttackAnimationCompleted()
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
}
