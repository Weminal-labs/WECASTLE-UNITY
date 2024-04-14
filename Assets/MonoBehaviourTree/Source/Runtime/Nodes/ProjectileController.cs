using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damage;
    public string tag;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            if (this.gameObject.CompareTag("Bomb"))
            {
                other.gameObject.GetComponent<EnemyController>().takeDame(damage);
            }
            else
            {
                other.gameObject.GetComponent<EnemyController>().takeDame(damage);
                GameObject.Destroy(this.gameObject);
            }

        }

    }

    public void SetDamage(int damage)
    { this.damage = damage; }

    public void die()
    {
        Destroy(this.gameObject);
    }

}
