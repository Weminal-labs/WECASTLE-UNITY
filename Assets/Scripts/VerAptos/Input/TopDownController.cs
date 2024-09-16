using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody rb;
    private HeroStats Stats;

    private bool isFacingRight;
    private Vector2 movement;

    [SerializeField]
    private bool isAttack;

    private IEnumerator attackCoroutine;

    [SerializeField]
    private float attackRange = 2f;
    [SerializeField]
    private float attackAngle = 60f;
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private JoyStickComponent joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Stats = this.GetComponent<HeroStats>();
        isFacingRight = true;
        isAttack = false;
        attackCoroutine = AutoAttack();
        StartCoroutine(attackCoroutine);
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        SetPos();
    }

    public void GetInput()
    {
        // Get keyboard input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Override with joystick input if present
        if (joystick.joyStickVec != Vector2.zero && !isAttack)
        {
            movement = joystick.joyStickVec;
        }

        Flip();
    }

    public void SetPos()
    {
        if (movement != Vector2.zero && !isAttack)
        {
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0); // Convert to Vector3
            rb.MovePosition(rb.position + movement3D * Stats.getSpeed() * Time.fixedDeltaTime);
            this.transform.GetChild(0).GetComponent<AnimationController>().setRunAnimation(1);
        }else{
            rb.velocity = Vector2.zero;
            this.transform.GetChild(0).GetComponent<AnimationController>().setRunAnimation(0);
        }
    }

    public void Flip()
    {
        if(!isAttack)
        {
            if ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight))
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                isFacingRight = !isFacingRight;
            }
        }
    }
    private IEnumerator AutoAttack()
    {
        while (true)
        {
            isAttack = false;
            yield return new WaitForSeconds(2f);
            if (IsEnemyInCone())
            {
                isAttack = true;
                // Perform attack
                this.transform.GetChild(0).GetComponent<AnimationController>().setBeginAttackAnimation();
                yield return new WaitForSeconds(0.8f);
                DealDamageToEnemiesInRange();
            }
        }
    }

    private bool IsEnemyInCone()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        return hitColliders.Any(c => IsInAttackCone(c.transform.position));
    }
    public void setAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
    private bool IsInAttackCone(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angle = Vector3.Angle(transform.right, directionToTarget);
        return angle <= attackAngle / 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.right;

        Vector3 leftDirection = Quaternion.Euler(0, 0, attackAngle / 2) * direction;
        Vector3 rightDirection = Quaternion.Euler(0, 0, -attackAngle / 2) * direction;

        Gizmos.DrawLine(transform.position, transform.position + leftDirection * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * attackRange);
        int segments = 20;
        Vector3 previousPoint = transform.position + rightDirection * attackRange;
        for (int i = 1; i <= segments; i++)
        {
            float angle = -attackAngle / 2 + (attackAngle * i / segments);
            Vector3 currentDirection = Quaternion.Euler(0, 0, angle) * direction;
            Vector3 currentPoint = transform.position + currentDirection * attackRange;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

    }
    private void DealDamageToEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            EnemyControllerVerAptos enemyHealth = hitCollider.GetComponent<EnemyControllerVerAptos>();
            if (enemyHealth != null)
            {
                enemyHealth.takeDame(Stats.getAttack());
            }
        }
    }
}
