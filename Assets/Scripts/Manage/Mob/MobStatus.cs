using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobStatus : MonoBehaviour, MobDataPersistance
{
    [SerializeField]
    private GameObject infoMob;
    private MobStats stats;
    [SerializeField]
    private int curHealth, maxHealth, damage, speed, exp, maxExp, lv, type;
    [SerializeField]
    private string name, history, id;

    Blackboard blackboard ;
    BoolVariable isDead;
    IntVariable blackBoardDamage;

    //DoubleClick to Open
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;
    public bool CompareData(string id)
    {
        if(this.id.CompareTo(id) == 0)
        {
            return true;
        }else
            return false;
    }
    public int getType()
    {
        return type;
    }

    public void LoadData(MobStats data)
    {
        this.id = data.getId();
        this.curHealth = data.getHealth();
        this.maxHealth = data.getMaxHealth();
        this.damage = data.getDamage();
        this.speed = data.getSpeed();
        this.gameObject.GetComponent<NavMeshAgent>().speed = this.speed;
        this.name = data.getName();
        this.exp = data.getExp();
        this.maxExp = data.getMaxExp();
        this.lv = data.getLevel();
        this.history = data.getHistory();
        this.type = data.getMobType();
        this.stats = data;
    }
    public void SaveData(ref MobStats data,int exp)
    {
        data.setExp(exp);
        data.setPos(this.gameObject.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        infoMob = GameObject.FindWithTag("MobInfo");
        StartCoroutine(openMobStatus());
        infoMob.GetComponent<LoadMobInfo>().LoadData(stats);
        blackboard = this.GetComponent<Blackboard>();
        isDead = blackboard.GetVariable<BoolVariable>("isDead");
        blackBoardDamage = blackboard.GetVariable<IntVariable>("Damage");
        blackBoardDamage.Value = damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator openMobStatus()
    {
        LoadData(this.stats);
        yield return new WaitForSeconds(1.5f);
        showInfoMob();
    }
    public void showInfoMob()
    {
        if(infoMob != null) 
        { 
            infoMob.GetComponent<LoadMobInfo>().LoadData(stats);
            infoMob.GetComponent<LoadMobInfo>().OpenMenu();
        }
    }
    private void OnMouseUp()
    {
        leftClickNum += 1;
        if (leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleClick());
            isDoubleClick = false;
        }
    }
    public void LvUp(int exp)
    {
        this.stats.setExp(exp);
        LoadData(this.stats);
    }
    IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while (Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if (leftClickNum == 2)
            {
                showInfoMob() ;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
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

    public string getIDMob()
    {
        return stats.getId();
    }
}
