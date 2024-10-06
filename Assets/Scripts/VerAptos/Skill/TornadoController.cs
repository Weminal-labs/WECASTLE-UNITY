using UnityEngine;
using System.Collections.Generic;

public class TornadoController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float circularMotionRadius = 2f;

    [Header("Lifetime")]
    [SerializeField] private float lifetime = 2f;

    [Header("Damage Settings")]
    [SerializeField] private float damageInterval = 0.2f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private Vector3 moveDirection;
    private float lifetimeTimer;
    private HashSet<Collider> damagedEnemies = new HashSet<Collider>();
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
        lifetimeTimer = 0f;
    }

    private void Update()
    {
        // Update timers
        lifetimeTimer += Time.deltaTime;

        if (lifetimeTimer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        // Move and rotate the tornado
        MoveTornado();

        // Deal damage
        DealDamageToEnemies();
    }

    private void MoveTornado()
    {
        if(PauseGameManager.instance.IsPaused()){
            return;
        }
        // Move the tornado
        Vector2 movement = moveDirection * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0);
    }

    private void DealDamageToEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circularMotionRadius, enemyLayer);
        // Cache the HeroStats component
        HeroStats heroStats = GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>();
        int damage = heroStats.GetAttack();
        int levelSkill = heroStats.getLevelUpList()[4];
        int totalDamage = damage * levelSkill;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out EnemyControllerVerAptos enemy))
            {
                enemy.takeDame(totalDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, circularMotionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, moveDirection * 2f);
    }
}