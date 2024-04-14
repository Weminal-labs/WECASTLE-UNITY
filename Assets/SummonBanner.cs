using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class SummonBanner : MonoBehaviour
{
    [SerializeField]
    private GameObject managerDataMob;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private GameObject summonCanvas, anim;
    [SerializeField]
    private Button buttonClose, buttonSummon, unitWarrior, unitArcher, unitPawn;
    [SerializeField]
    private Sprite selectUnit, unSelectUnit;
    [SerializeField]
    private GameObject health, damage, speed, meat, gold, wood;
    private TextMeshProUGUI healthText, damageText, speedText, meatCost, goldCost, woodCost;
    [SerializeField]
    private RuntimeAnimatorController wAnim, aAnim, pAnim;
    [SerializeField]
    private GameObject name, history;
    [SerializeField]
    private GameObject[] spawnUnit;
    private int unit;
    [SerializeField]
    private GameObject PlaceHolder, Animation, gameControll;
    // Start is called before the first frame update
    void Start()
    {
        unit = 0;
        healthText = health.GetComponent<TextMeshProUGUI>();
        damageText = damage.GetComponent<TextMeshProUGUI>();
        speedText = speed.GetComponent<TextMeshProUGUI>();
        woodCost = wood.GetComponent<TextMeshProUGUI>();
        meatCost = meat.GetComponent<TextMeshProUGUI>();
        goldCost = gold.GetComponent<TextMeshProUGUI>();
        selectWarrior();
        if (buttonClose != null)
        {
            buttonClose.onClick.AddListener(Close);
        }
        if (buttonSummon != null)
        {
            buttonSummon.onClick.AddListener(Summon);
        }
        if (unitWarrior != null)
        {
            unitWarrior.onClick.AddListener(selectWarrior);
        }
        if (unitArcher != null)
        {
            unitArcher.onClick.AddListener(selectArcher);
        }
        if (unitPawn != null)
        {
            unitPawn.onClick.AddListener(selectPawn);
        }
    }

    private void Close()
    {
        name.GetComponentInChildren<TMP_InputField>().text = "";
        history.GetComponentInChildren<TMP_InputField>().text = "";
        if (summonCanvas != null)
        {
            summonCanvas.gameObject.SetActive(false);
        }
    }
    private void Summon()
    {
        string[] split = healthText.text.Split('-');
        int minHealth = Int32.Parse(split[0]);
        int maxHealth = Int32.Parse(split[1]);
        split = damageText.text.Split('-');
        int minDamage = Int32.Parse(split[0]);
        int maxDamage = Int32.Parse(split[1]);
        split = speedText.text.Split('-');
        int minSpeed = Int32.Parse(split[0]);
        int maxSpeed = Int32.Parse(split[1]);
        string nameMob = name.GetComponentInChildren<TMP_InputField>().text;
        string historyMob = history.GetComponentInChildren<TMP_InputField>().text;
        MobStats mob = new MobStats(this.unit, minHealth, maxHealth, minDamage, maxDamage, minSpeed, maxSpeed, nameMob, historyMob);
        MobStats mobSummon = new MobStats(mob, spawnPosition.transform.position);
        managerDataMob.GetComponent<ManageMobData>().addMob(mobSummon);
        StartCoroutine(StartAnimation(mobSummon));
        gameControll.GetComponent<GameController>().setMaterials(-Int32.Parse(meatCost.text), -Int32.Parse(goldCost.text), -Int32.Parse(woodCost.text));
    }
    private void selectWarrior()
    {
        unit = 0;
        unitWarrior.GetComponent<Image>().sprite = selectUnit;
        unitArcher.GetComponent<Image>().sprite = unSelectUnit;
        unitPawn.GetComponent<Image>().sprite = unSelectUnit;
        int lvP = gameControll.GetComponent<GameController>().getPlayer().getLv();
        int minHealth = 140 + (lvP-1) * 3;
        int maxHealth = 200 + (lvP - 1) * 3;
        int minDamage = 25 + (lvP - 1) * 3;
        int maxDamage = 50 + (lvP - 1) * 3;
        healthText.SetText(minHealth+"-"+maxHealth);
        damageText.SetText(minDamage+"-"+maxDamage);
        speedText.SetText("4-7");
        meatCost.SetText("40");
        woodCost.SetText("15");
        goldCost.SetText("20");
        if (checkM())
        {
            buttonSummon.gameObject.SetActive(true);
        }
        else
        {
            buttonSummon.gameObject.SetActive(false);
        }
        anim.GetComponent<Animator>().runtimeAnimatorController = wAnim as RuntimeAnimatorController;
    }
    private void selectArcher()
    {
        unit = 1;
        unitWarrior.GetComponent<Image>().sprite = unSelectUnit;
        unitArcher.GetComponent<Image>().sprite = selectUnit;
        unitPawn.GetComponent<Image>().sprite = unSelectUnit;
        int lvP = gameControll.GetComponent<GameController>().getPlayer().getLv();
        int minHealth = 60 + (lvP - 1) * 3;
        int maxHealth = 80 + (lvP - 1) * 3;
        int minDamage = 40 + (lvP - 1) * 3;
        int maxDamage = 70 + (lvP - 1) * 3;
        healthText.SetText(minHealth + "-" + maxHealth);
        damageText.SetText(minDamage + "-" + maxDamage);
        speedText.SetText("5-8");
        meatCost.SetText("30");
        woodCost.SetText("30");
        goldCost.SetText("30");
        if (checkM())
        {
            buttonSummon.gameObject.SetActive(true);
        }
        else
        {
            buttonSummon.gameObject.SetActive(false);
        }
        anim.GetComponent<Animator>().runtimeAnimatorController = aAnim as RuntimeAnimatorController;
    }
    private void selectPawn()
    {
        unit = 2;
        unitWarrior.GetComponent<Image>().sprite = unSelectUnit;
        unitArcher.GetComponent<Image>().sprite = unSelectUnit;
        unitPawn.GetComponent<Image>().sprite = selectUnit;
        int lvP = gameControll.GetComponent<GameController>().getPlayer().getLv();
        int minHealth = 40 + (lvP - 1) * 3;
        int maxHealth = 60 + (lvP - 1) * 3;
        int minDamage = 20 + (lvP - 1) * 3;
        int maxDamage = 30 + (lvP - 1) * 3;
        healthText.SetText(minHealth + "-" + maxHealth);
        damageText.SetText(minDamage + "-" + maxDamage);
        speedText.SetText("6-8");
        meatCost.SetText("20");
        woodCost.SetText("20");
        goldCost.SetText("15");
        if (checkM())
        {
            buttonSummon.gameObject.SetActive(true);
        }
        else
        {
            buttonSummon.gameObject.SetActive(false);
        }
        anim.GetComponent<Animator>().runtimeAnimatorController = pAnim as RuntimeAnimatorController;
    }

    IEnumerator StartAnimation(MobStats data)
    {
        PlaceHolder.gameObject.SetActive(false);
        var tempColor = this.gameObject.GetComponent<RawImage>().color;
        tempColor.a = 0f;
        this.gameObject.GetComponent<RawImage>().color = tempColor;
        Animation.GetComponent<Animator>().SetTrigger("StartSummon");
        yield return new WaitForSeconds(4.5f);
        Instantiate(spawnUnit[unit], spawnPosition.transform.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(data);
        //This is code for Connect to server to get new ID
        MobStatsForJSON json = new MobStatsForJSON(data);
        RequestID(JsonConvert.SerializeObject(json), data.getId());
        tempColor = this.gameObject.GetComponent<RawImage>().color;
        tempColor.a = 100.0f;
        this.gameObject.GetComponent<RawImage>().color = tempColor;
        PlaceHolder.gameObject.SetActive(true);
        this.gameObject.gameObject.SetActive(false);
    }
    public bool checkM()
    {
        if (gameControll.GetComponent<GameController>().getPlayer().getCurGold() < Int32.Parse(goldCost.text) || gameControll.GetComponent<GameController>().getPlayer().getCurWood() < Int32.Parse(woodCost.text) || gameControll.GetComponent<GameController>().getPlayer().getCurMeat() < Int32.Parse(meatCost.text))
        {
            return false;
        }
        return true;
    }
    [DllImport("__Internal")]
    public static extern void RequestID(string json, string fakeId);
}
