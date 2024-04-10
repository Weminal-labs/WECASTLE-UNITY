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

    [SerializeField]
    private GameObject m, g;

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
        int gold = Random.Range(1, 6);
        int meat = Random.Range(1, 6);
        float x, y;
        for (int i = 0; i < gold; i++)
        {
            x = Random.Range(this.transform.position.x - 2, this.transform.position.x + 2);
            y = Random.Range(this.transform.position.y - 2, this.transform.position.y + 2);
            Instantiate(g, new Vector3(x, y, transform.position.z), Quaternion.identity);
        }
        for (int i = 0; i < meat; i++)
        {
            x = Random.Range(this.transform.position.x - 2, this.transform.position.x + 2);
            y = Random.Range(this.transform.position.y - 2, this.transform.position.y + 2);
            Instantiate(m, new Vector3(x, y, transform.position.z), Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    public void spawnSet(int lvMob)
    {
        this.maxHealth = Random.Range(75, 180);
        this.curHealth = maxHealth;
        this.damage = Random.Range(30,45);
        this.speed = 12;
        for(; lvMob>0;)
        {
            this.maxHealth += Random.Range(3, 8);
            this.curHealth = maxHealth;
            this.damage += Random.Range(3, 8);
            lvMob--;
        }
    }
}
