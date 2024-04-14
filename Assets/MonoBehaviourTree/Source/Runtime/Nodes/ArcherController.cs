using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public GameObject arrowPrefap;
    public float arrowSpeed = 10f;
    public float arrowLifetime = 3f;
    public Vector3 direction;
    private Blackboard blackboard;
    private FloatReference angle;
    private TransformReference target;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }

/*    public void SpawnArrow()
    {
        Transform targetTransform = target.Value;
        direction = targetTransform.position - transform.position;
        float angleTarget = angle.Value;
        // Spawn arrow at archer's position
        GameObject arrow = GameObject.Instantiate(arrowPrefap, transform.position, Quaternion.identity);
        print(GetComponent<MobStatus>().getDamage());
        arrow.GetComponent<ProjectileController>().SetDamage(this.GetComponent<MobStatus>().getDamage());
        // Rotate the arrow to face the enemy
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angleTarget);

        // Move the arrow in the direction of the enemy (optional)
        arrow.GetComponent<Rigidbody>().velocity = direction.normalized * arrowSpeed;
        GameObject.Destroy(arrow, arrowLifetime);
    }*/



}
