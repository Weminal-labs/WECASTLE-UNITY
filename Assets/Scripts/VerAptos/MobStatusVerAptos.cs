using MBT;
using UnityEngine;
using UnityEngine.AI;

public class MobStatusVerAptos : MonoBehaviour
{
    [SerializeField]
    private int curHealth, maxHealth, damage, speed;
    Blackboard blackboard;
    BoolVariable isDead;
    IntVariable blackBoardDamage;
    private NavMeshAgent Agent;



    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        blackboard = this.GetComponent<Blackboard>();
        isDead = blackboard.GetVariable<BoolVariable>("isDead");
        blackBoardDamage = blackboard.GetVariable<IntVariable>("Damage");
        blackBoardDamage.Value = damage;

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



    public void die()
    {
        Destroy(this.gameObject);
    }

}
