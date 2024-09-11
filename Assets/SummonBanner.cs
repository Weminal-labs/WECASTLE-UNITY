using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private void Update()
    {
        if (checkM())
        {
            buttonSummon.gameObject.SetActive(true);
        }
        else
        {
            buttonSummon.gameObject.SetActive(false);
        }
    }
    private void Close()
    {
        if (summonCanvas != null)
        {
            summonCanvas.gameObject.SetActive(false);
        }
    }
    private void Summon()
    {
        MobStats mob = new MobStats(this.unit, Int32.Parse(healthText.text), Int32.Parse(damageText.text), Int32.Parse(speedText.text));
        MobStats mobSummon = new MobStats(mob, spawnPosition.transform.position);
        mobSummon.setInBuilding(true);
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
        int maxHealth = 400;
        int maxDamage = 50;
        healthText.SetText(maxHealth.ToString());
        damageText.SetText(maxDamage.ToString());
        speedText.SetText("5");
        meatCost.SetText("40");
        woodCost.SetText("15");
        goldCost.SetText("20");
        name.GetComponent<TextMeshProUGUI>().SetText("Warrior");
        history.GetComponent<TextMeshProUGUI>().SetText("- High HP\n- High Damage\n- Melee weapon");
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
        int maxHealth = 80;
        int maxDamage = 60;
        healthText.SetText(maxHealth.ToString());
        damageText.SetText(maxDamage.ToString());
        speedText.SetText("7");
        meatCost.SetText("30");
        woodCost.SetText("30");
        goldCost.SetText("30");
        name.GetComponent<TextMeshProUGUI>().SetText("Archer");
        history.GetComponent<TextMeshProUGUI>().SetText("- Low HP\n- Low Damage\n- Can attack with long range");
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
        int maxHealth = 40;
        int maxDamage = 20;
        healthText.SetText(maxHealth.ToString());
        damageText.SetText(maxDamage.ToString());
        speedText.SetText("7");
        meatCost.SetText("15");
        woodCost.SetText("15");
        goldCost.SetText("15");
        name.GetComponent<TextMeshProUGUI>().SetText("Pawn");
        history.GetComponent<TextMeshProUGUI>().SetText("- Low HP\n- Low Damage\n- Need for gather materials");
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
        data.setInBuilding(false);
        Instantiate(spawnUnit[unit], spawnPosition.transform.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(data);
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
    /*[DllImport("__Internal")]
    public static extern void RequestID(string json, string fakeId);*/
}
