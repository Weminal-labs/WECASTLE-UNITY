using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public int damage;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().takeDame(damage);
            GameObject.Destroy(this.gameObject);
        }

    }

    public void SetDamage(int damage)
    { this.damage = damage; }
}
