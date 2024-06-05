using MBT;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobStatus : MonoBehaviour, MobDataPersistance
{
    [SerializeField]
    private GameObject infoMob;
    private MobStats stats;
    [SerializeField]
    private int curHealth, maxHealth, damage, speed, type;
    [SerializeField]
    private string id;
    Blackboard blackboard;
    BoolVariable isDead;
    IntVariable blackBoardDamage;
    BoolVariable block;
    BoolVariable? needHeal;


    //DoubleClick to Open
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;
    public bool CompareData(string id)
    {
        if (this.id.CompareTo(id) == 0)
        {
            return true;
        }
        else
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
        this.type = data.getMobType();
        this.stats = data;
    }
    public void SaveData(ref MobStats data)
    {

    }
    public MobStats GetMobStats()
    {
        return this.stats;
    }
    // Start is called before the first frame update
    void Start()
    {
        infoMob = GameObject.FindWithTag("MobInfo");
        /*StartCoroutine(openMobStatus());*/
        infoMob.GetComponent<LoadMobInfo>().LoadData(stats);
        blackboard = this.GetComponent<Blackboard>();
        isDead = blackboard.GetVariable<BoolVariable>("isDead");
        blackBoardDamage = blackboard.GetVariable<IntVariable>("Damage");
        blackBoardDamage.Value = damage;
        /* block = blackboard.GetVariable<BoolVariable>("Block");*/
        needHeal = blackboard.GetVariable<BoolVariable>("needHeal");

    }

    // Update is called once per frame
    void Update()
    {
        if (needHeal != null)
            NeedHeal();
    }

    public void NeedHeal()
    {
        if (curHealth < maxHealth)
        {
            needHeal.Value = true;
        }
        else
        {
            needHeal.Value = false;
        }
    }

    IEnumerator openMobStatus()
    {
        LoadData(this.stats);
        yield return new WaitForSeconds(1.5f);
        showInfoMob();
    }
    public void showInfoMob()
    {
        if (infoMob != null)
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
    IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while (Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if (leftClickNum == 2)
            {
                showInfoMob();
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
        /*if (block.Value == true)
        {
            return;
        }*/
        /*return;*/
        print(damage);
        if (curHealth - damage < 0)
        {
            isDead.Value = true;
            stats.setHealth(-damage);
            /*GameObject.FindGameObjectWithTag("MOBDATA").GetComponent<ManageMobData>().saveMob();*/
        }
        else
        {
            curHealth -= damage;
            DamageEffect damageEffect = GetComponent<DamageEffect>();
            stats.setHealth(-damage);
            if (damageEffect != null)
            {
                damageEffect.Flash();
            }
        }
    }

    public void heal()
    {
        /*if (block.Value == true)
        {
            return;
        }*/
        /*return;*/

        if (curHealth + damage > maxHealth)
        {
            stats.setHealth(maxHealth - curHealth);

            curHealth = maxHealth;
        }
        if (curHealth < maxHealth)
        {
            stats.setHealth(damage);

            curHealth += damage;
        }

    }

    public void mobOutBuilding()
    {
        stats.setInBuilding(false);
    }
    public void die()
    {
        SelectionManager.Instance.Deselect(this.gameObject.GetComponent<SelectableUnit>());
        Destroy(this.gameObject);
    }

    public string getIDMob()
    {
        return stats.getId();
    }
    public void rainWeather()
    {
        damage += damage / 10;
        speed -= 2;
    }
}
