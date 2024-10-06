using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMaster : MonoBehaviour
{
    [Header("Attack Properties")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 60f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Components")]
    [SerializeField] private AnimationController animationController;

    [Header("Movement & Stats")]
    [SerializeField] private TopDownController topDownController;
    [SerializeField] private HeroStats heroStats;
    private bool isAttacking = false;
    private float lastAttackTime;
    private bool isFacingRight = true;

    private void Start()
    {
        heroStats = this.gameObject.transform.parent.gameObject.GetComponent<HeroStats>();
        topDownController = this.gameObject.transform.parent.gameObject.GetComponent<TopDownController>();
        lastAttackTime = -attackCooldown;
        StartCoroutine(AutoAttackCoroutine());
    }

    private IEnumerator AutoAttackCoroutine()
    {
        while (true)
        {
            if (!isAttacking && Time.time >= lastAttackTime + attackCooldown && IsEnemyInCone())
            {
                yield return StartCoroutine(PerformAttack());
            }
            yield return null;
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        topDownController.SetAttack(true);
        lastAttackTime = Time.time;
        if (heroStats.getLevelUpList()[2] >= 10)
        {
            attackCooldown = 0.15f;
            attackDuration = 0.15f;
        }
        else
        {
            attackCooldown = 2f - 0.1f * heroStats.getLevelUpList()[2];
            attackDuration = 0.5f - 0.05f*heroStats.getLevelUpList()[2];
        }

        animationController.setBeginAttackAnimation();
        yield return new WaitForSeconds(attackDuration/2);
        DealDamageToEnemiesInCone();
        yield return new WaitForSeconds(attackDuration/2);
        DealDamageToEnemiesInCone();
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        topDownController.SetAttack(false);
    }

    private bool IsEnemyInCone()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        return System.Array.Exists(hitColliders, c => IsInAttackCone(c.transform.position));
    }

    private void DealDamageToEnemiesInCone()
    {
        Vector2 pos = transform.position;
        pos.x -= 0.1f;
        Collider[] hitColliders = Physics.OverlapSphere(pos, attackRange, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (IsInAttackCone(hitCollider.transform.position))
            {
                if (hitCollider.TryGetComponent(out EnemyControllerVerAptos enemyHealth))
                {
                    enemyHealth.takeDame(heroStats.GetAttack()*heroStats.getLevelUpList()[2]);
                }
            }
        }
    }

    private bool IsInAttackCone(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        Vector3 attackDirection = topDownController.isFacingRight ? transform.right : -transform.right;
        float angle = Vector3.Angle(attackDirection, directionToTarget);
        return angle <= attackAngle / 2;
    }

    public bool IsAttacking() => isAttacking;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 attackDirection = isFacingRight ? transform.right : -transform.right;

        Vector3 leftDirection = Quaternion.Euler(0, 0, attackAngle / 2) * attackDirection;
        Vector3 rightDirection = Quaternion.Euler(0, 0, -attackAngle / 2) * attackDirection;

        Vector3 pos = transform.position;
        pos.x -= 0.1f;
        Gizmos.DrawLine(pos, pos + leftDirection * attackRange);
        Gizmos.DrawLine(pos, pos + rightDirection * attackRange);

        int segments = 20;
        Vector3 previousPoint = transform.position + rightDirection * attackRange;
        for (int i = 1; i <= segments; i++)
        {
            float angle = -attackAngle / 2 + (attackAngle * i / segments);
            Vector3 currentDirection = Quaternion.Euler(0, 0, angle) * attackDirection;
            Vector3 currentPoint = transform.position + currentDirection * attackRange;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }
}
