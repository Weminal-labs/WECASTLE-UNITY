using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityHFSM;  // Import UnityHFSM


public class GuardAI : MonoBehaviour
{
    public static GuardAI Instance { get; private set; }

    private NavMeshAgent Agent;

    // Declare the finite state machine
    private StateMachine fsm;

    // Parameters (can be changed in the inspector)
    public float searchSpotRange = 10;
    public float attackRange = 3;

    public float searchTime = 10;  // in seconds

    public float patrolSpeed = 2;
    public float chaseSpeed = 4;
    public float attackSpeed = 2;

    public Vector3[] patrolPoints;

    //public List<string> potentialTargetTags;  // List of potential targets (game objects)
    //public Transform target;  // Reference to the closest player (target)

    // Internal fields
    private Animator animator;
    private int patrolDirection = 1;
    private Vector3 lastSeenPlayerPosition;

    // Helper methods (depend on how your scene has been set up)
    private Vector3 playerPosition => EnemyAI.Instance.transform.position;
    private float distanceToPlayer => Vector3.Distance(playerPosition, transform.position);



    void Start()
    {
        Instance = this;

        Agent = GetComponent<NavMeshAgent>();

            animator = GetComponent<Animator>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            fsm = new StateMachine();

        // Fight FSM
        var fightFsm = new HybridStateMachine(
            beforeOnLogic: state => MoveTowards(playerPosition, attackSpeed, minDistance: 1.5f),
            needsExitTime: true
        );

        fightFsm.AddState("Wait", onEnter: state => animator.Play("Idle"));
        fightFsm.AddState("Hit1", onEnter: state => animator.Play("AttackSide_1"));
        fightFsm.AddState("Hit2",
            onEnter: state =>
            {
                animator.Play("AttackSide_2");
                // TODO: Cause damage to player if in range.
            }
        );

        // Because the exit transition should have the highest precedence,
        // it is added before the other transitions.
        fightFsm.AddExitTransition("Wait");

        fightFsm.AddTransition(new TransitionAfter("Wait", "Hit1", 0.5f));
        fightFsm.AddTransition(new TransitionAfter("Hit1", "Hit2", 0.5f));
        fightFsm.AddTransition(new TransitionAfter("Hit2", "Wait", 0.5f));

        // Root FSM
        fsm.AddState("Patrol", new CoState(this, Patrol, loop: false));
        fsm.AddState("Chase", new State(
            onLogic: state => MoveTowards(playerPosition, chaseSpeed)
        ));
        fsm.AddState("Fight", fightFsm);
        fsm.AddState("Search", new CoState(this, Search, loop: false));

        fsm.SetStartState("Patrol");

        fsm.AddTriggerTransition("PlayerSpotted", "Patrol", "Chase");
        fsm.AddTwoWayTransition("Chase", "Fight", t => distanceToPlayer <= attackRange);
        fsm.AddTransition("Chase", "Search",
            t => distanceToPlayer > searchSpotRange,
            onTransition: t => lastSeenPlayerPosition = playerPosition);
        fsm.AddTransition("Search", "Chase", t => distanceToPlayer <= searchSpotRange);
        fsm.AddTransition(new TransitionAfter("Search", "Patrol", searchTime));

        fsm.Init();
    }

    void Update()
    {
        //FindClosestPlayer();
        fsm.OnLogic();
        Debug.Log(fsm.ActiveStateName);
    }

    // Triggers the `PlayerSpotted` event.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            fsm.Trigger("PlayerSpotted");
        }
    }

    private void MoveTowards(Vector3 target, float speed, float minDistance = 0)
    {
        animator.Play("Run");
        target.z = 0;
        // Set the destination for the agent
        if(Agent.remainingDistance >= minDistance)
            Agent.SetDestination(target);

        // Set the agent speed
        Agent.speed = speed;

    }



    private IEnumerator MoveToPosition(Vector3 target, float speed, float tolerance=0.05f)
    {
        while (Vector3.Distance(transform.position, target) > tolerance)
        {
            MoveTowards(target, speed);
            // Wait one frame.
            yield return null;
        }
    }

    private IEnumerator Patrol()
    {
        int currentPointIndex = FindClosestPatrolPoint();

            while (true)
        {
            yield return MoveToPosition(patrolPoints[currentPointIndex], patrolSpeed);
                animator.Play("Idle");

                // Wait at each patrol point.
                yield return new WaitForSeconds(3);

                currentPointIndex += patrolDirection;

            // Once the bot reaches the end or the beginning of the patrol path,
            // it reverses the direction.
            if (currentPointIndex >= patrolPoints.Length || currentPointIndex < 0)
            {
                currentPointIndex = Mathf.Clamp(currentPointIndex, 0, patrolPoints.Length-1);
                patrolDirection *= -1;
            }
        }
    }

    private int FindClosestPatrolPoint()
    {
        float minDistance = Vector2.Distance(transform.position, patrolPoints[0]);
        int minIndex = 0;

        for (int i = 1; i < patrolPoints.Length; i ++)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        return minIndex;
    }

    private IEnumerator Search()
    {
        yield return MoveToPosition(lastSeenPlayerPosition, chaseSpeed);

        while (true)
        {
            yield return new WaitForSeconds(2);

            yield return MoveToPosition(
                (Vector2)transform.position + Random.insideUnitCircle * 10,
                patrolSpeed
            );
        }
    }
}

