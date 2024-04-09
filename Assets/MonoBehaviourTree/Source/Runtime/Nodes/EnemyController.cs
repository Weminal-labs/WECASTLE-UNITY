using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private int curHealth, maxHealth, damage, speed;

    Blackboard blackboard;
    BoolVariable isDead;
    Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        blackboard = this.GetComponent<Blackboard>();
        isDead = blackboard.GetVariable<BoolVariable>("isDead");
        collider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FlipLeft()
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.y, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    public void FlipRight()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.y, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    public int getDamage()
    {
        return damage;
    }

    public void takeDame(int damage)
    {
        if (curHealth - damage < 0)
        {
            isDead.Value = true;
        }
        else
        {
            curHealth -= damage;
            DamageEffect damageEffect = GetComponent<DamageEffect>();
            if (damageEffect != null)
            {
                damageEffect.Flash();
            }
        }

    }

    public void die()
    {
        Destroy(this.gameObject);
    }
}
