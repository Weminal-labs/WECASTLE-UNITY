using MBT;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerVerAptos : MonoBehaviour
{
    private NavMeshAgent Agent;

    [SerializeField]
    private int curHealth, maxHealth, damage, speed, exp;


    [SerializeField]
    private int type;
    Blackboard blackboard;
    BoolVariable isDead;
    IntVariable blackBoardDamage;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        calmaxHealth();
        blackboard = this.GetComponent<Blackboard>();
        isDead = blackboard.GetVariable<BoolVariable>("isDead");
        blackBoardDamage = blackboard.GetVariable<IntVariable>("Damage");
        blackBoardDamage.Value = damage;
    }
    public void calmaxHealth()
    {
        for(int i = 0; i< VerAptosController.instance.wave; i++)
        {
            maxHealth += i*5;
        }
        curHealth = maxHealth;
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
        return damage+3*VerAptosController.instance.wave;
    }

    public void takeDame(int damage)
    {
        if (curHealth - damage <= 0)
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
        VerAptosController.instance.updatePoints(type);
        Destroy(this.gameObject);
    }

    public void spawnExp()
    {
        // Find the HeroStats component
        HeroStats heroStats = FindObjectOfType<HeroStats>();
        if (heroStats != null)
        {
            // Call the AddExp method
            heroStats.SendMessage("AddExp", exp);
        }
    }
}
