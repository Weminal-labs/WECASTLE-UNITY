using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private GameObject PlaceHolder, Animation;
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
        MobStats mob = new MobStats(this.unit, minHealth,  maxHealth, minDamage, maxDamage, minSpeed, maxSpeed, nameMob, historyMob);
        MobStats mobSummon = new MobStats(mob, spawnPosition.transform.position);
        managerDataMob.GetComponent<ManageMobData>().addMob(mobSummon);
        StartCoroutine(StartAnimation(mobSummon));
    }
    private void selectWarrior()
    {
        unit = 0;
        unitWarrior.GetComponent<Image>().sprite = selectUnit;
        unitArcher.GetComponent<Image>().sprite = unSelectUnit;
        unitPawn.GetComponent<Image>().sprite = unSelectUnit;
        healthText.SetText("75-150");
        damageText.SetText("25-50");
        speedText.SetText("5-10");
        meatCost.SetText("60");
        woodCost.SetText("15");
        goldCost.SetText("30");
        anim.GetComponent<Animator>().runtimeAnimatorController = wAnim as RuntimeAnimatorController;
    }
    private void selectArcher()
    {
        unit = 1;
        unitWarrior.GetComponent<Image>().sprite = unSelectUnit;
        unitArcher.GetComponent<Image>().sprite = selectUnit;
        unitPawn.GetComponent<Image>().sprite = unSelectUnit;
        healthText.SetText("60-120");
        damageText.SetText("40-80");
        speedText.SetText("8-12");
        meatCost.SetText("60");
        woodCost.SetText("30");
        goldCost.SetText("30");
        anim.GetComponent<Animator>().runtimeAnimatorController = aAnim as RuntimeAnimatorController;
    }
    private void selectPawn()
    {
        unit = 2;
        unitWarrior.GetComponent<Image>().sprite = unSelectUnit;
        unitArcher.GetComponent<Image>().sprite = unSelectUnit;
        unitPawn.GetComponent<Image>().sprite = selectUnit;
        healthText.SetText("30-40");
        damageText.SetText("10-25");
        speedText.SetText("10-15");
        meatCost.SetText("40");
        woodCost.SetText("30");
        goldCost.SetText("20");
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
        MobStatsForJSON json = new MobStatsForJSON(data);
        Debug.Log(JsonConvert.SerializeObject(json));
        tempColor = this.gameObject.GetComponent<RawImage>().color;
        tempColor.a = 100.0f;
        this.gameObject.GetComponent<RawImage>().color = tempColor;
        PlaceHolder.gameObject.SetActive(true);
        this.gameObject.gameObject.SetActive(false);
    }
}
