using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public float attackAngleThreshold = 45f; // Threshold angle for attacking
    public string enemyTag = "Enemy";
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Find the nearest enemy
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            // Calculate the angle between the ally and the enemy
            Vector3 direction = nearestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Determine the direction to attack based on the angle
            if (Mathf.Abs(angle) < attackAngleThreshold)
            {
                // Attack to the right
                animator.SetTrigger("AttackRight");
            }
            else if (angle > 90 - attackAngleThreshold && angle < 90 + attackAngleThreshold)
            {
                // Attack upwards
                animator.SetTrigger("AttackUp");
            }
            else if (angle > 180 - attackAngleThreshold || angle < -180 + attackAngleThreshold)
            {
                // Attack to the left
                animator.SetTrigger("AttackLeft");
            }
            else if (angle < -90 + attackAngleThreshold && angle > -90 - attackAngleThreshold)
            {
                // Attack downwards
                animator.SetTrigger("AttackDown");
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
