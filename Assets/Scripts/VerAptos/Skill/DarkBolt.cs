using UnityEngine;

public class DarkBolt : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private LayerMask enemyLayer;

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Explode();
    // }

    public void Explode()
    {
        // Find all enemies within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (var hitCollider in hitColliders)
        {
            // Check if the hit object has an EnemyHealth component
            if (hitCollider.TryGetComponent(out EnemyControllerVerAptos enemyHealth))
            {
                // Calculate damage based on distance from explosion center
                Vector2 pos = this.transform.position;
                pos.y -=0.5f;
                float distance = Vector3.Distance(pos, hitCollider.transform.position);
                int damage = GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().GetAttack();
                int levelSkill = GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[3];
                int actualDamage = damage * levelSkill;

                // Apply damage to the enemy
                enemyHealth.takeDame(actualDamage);
            }
        }
    }
    public void Destroy(){
        Destroy(gameObject);
    }

    // Visualize the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 pos = this.transform.position;
        pos.y -=0.5f;
        Gizmos.DrawWireSphere(pos, explosionRadius);
    }
}