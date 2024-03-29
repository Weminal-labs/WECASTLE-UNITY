using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatus : MonoBehaviour, MobDataPersistance
{
    [SerializeField]
    private GameObject infoMob;
    private MobStats stats;
    [SerializeField]
    private int curHealth, maxHealth, damage, speed, exp, maxExp, lv;
    [SerializeField]
    private string name, history, id;


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

    public void LoadData(MobStats data)
    {
        this.id = data.getId();
        this.curHealth = data.getHealth();
        this.maxHealth = data.getMaxHealth();
        this.damage = data.getDamage();
        this.speed = data.getSpeed();
        this.name = data.getName();
        this.exp = data.getExp();
        this.maxExp = data.getMaxExp();
        this.lv = data.getLevel();
        this.history = data.getHistory();
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
        //infoMob.GetComponent<LoadMobInfo>().LoadData(stats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator openMobStatus()
    {
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
}
