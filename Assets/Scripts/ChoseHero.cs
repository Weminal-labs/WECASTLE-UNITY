using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseHero : MonoBehaviour
{
    [SerializeField]
    private bool[] isLock;
    public GameObject[] btnHero;
    public GameObject btnChose, price;
    public Animator review;
    public Animator reviewMain;
    [Header("===========Sprite==============")]
    public Sprite bannerSelect;
    public Sprite bannerUnSelect;
    public Sprite btnBuy;
    public Sprite btnSelect;
    [Header("===========Text==============")]
    public TextMeshProUGUI text;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDamage;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textSpeed;
    private int curIndex = 0;
    private int indexChose = 0;
    [Header("==========Animator=============")]
    public RuntimeAnimatorController[] animator;
    private void Start()
    {
        indexChose = 0;
        price.SetActive(false);
        if (btnChose != null) 
        {
            btnChose.GetComponent<Button>().onClick.AddListener(SetChose);
        }
        btnHero[0].GetComponent<Button>().onClick.AddListener(SetPreviewSoldier);
        btnHero[1].GetComponent<Button>().onClick.AddListener(SetPreviewKnight);
        btnHero[2].GetComponent<Button>().onClick.AddListener(SetPreviewTemplar);
        btnHero[3].GetComponent<Button>().onClick.AddListener(SetPreviewGuardian);
        btnHero[4].GetComponent<Button>().onClick.AddListener(SetPreviewSwordMaster);
        curIndex = 0;
    }
    public void SetPreviewSoldier()
    {
        review.runtimeAnimatorController = animator[0];
        textDamage.SetText("50");
        textHealth.SetText("3000");
        textSpeed.SetText("8");
        if (isLock[0])
        {
            btnChose.GetComponent<Image>().sprite = btnBuy;
            text.SetText("Chose");
        }
        else
        {
            btnChose.GetComponent<Image>().sprite = btnSelect;
            price.SetActive(false);
            text.SetText("Chose");
        }
        curIndex = 0;
        if (curIndex == indexChose && !isLock[curIndex])
        {
            btnChose.SetActive(false);
        }
        else
        {
            btnChose.SetActive(true);
        }
    }
    public void SetPreviewKnight()
    {
        review.runtimeAnimatorController = animator[1];
        textDamage.SetText("90");
        textHealth.SetText("4500");
        textSpeed.SetText("30");
        if (isLock[1])
        {
            btnChose.GetComponent<Image>().sprite = btnBuy;
            price.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "3000";
            price.SetActive(true);
            text.SetText("Buy");
        }
        else
        {
            btnChose.GetComponent<Image>().sprite = btnSelect;
            price.SetActive(false);
            text.SetText("Chose");
        }
        curIndex = 1;
        if (curIndex == indexChose && !isLock[curIndex])
        {
            btnChose.SetActive(false);
        }
        else
        {
            btnChose.SetActive(true);
        }
    }
    public void SetPreviewTemplar()
    {
        review.runtimeAnimatorController = animator[2];
        textDamage.SetText("40");
        textHealth.SetText("12000");
        textSpeed.SetText("5");
        if (isLock[2])
        {
            btnChose.GetComponent<Image>().sprite = btnBuy;
            price.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "5000";
            price.SetActive(true);
            text.SetText("Buy");
        }
        else
        {
            btnChose.GetComponent<Image>().sprite = btnSelect;
            price.SetActive(false);
            text.SetText("Chose");
        }
        curIndex = 2;
        if (curIndex == indexChose && !isLock[curIndex])
        {
            btnChose.SetActive(false);
        }
        else
        {
            btnChose.SetActive(true);
        }
    }
    public void SetPreviewGuardian()
    {
        review.runtimeAnimatorController = animator[3];
        textDamage.SetText("90");
        textHealth.SetText("6000");
        textSpeed.SetText("6");
        if (isLock[3])
        {
            btnChose.GetComponent<Image>().sprite = btnBuy;
            price.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "8000";
            price.SetActive(true);
            text.SetText("Buy");
        }
        else
        {
            btnChose.GetComponent<Image>().sprite = btnSelect;
            price.SetActive(false);
            text.SetText("Chose");
        }
        curIndex = 3;
        if (curIndex == indexChose && !isLock[curIndex])
        {
            btnChose.SetActive(false);
        }
        else
        {
            btnChose.SetActive(true);
        }
    }
    public void SetPreviewSwordMaster()
    {
        review.runtimeAnimatorController = animator[4];
        textDamage.SetText("120");
        textHealth.SetText("4000");
        textSpeed.SetText("8");
        if (isLock[4])
        {
            btnChose.GetComponent<Image>().sprite = btnBuy;
            price.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "9000";
            price.SetActive(true);
            text.SetText("Buy");
        }
        else
        {
            btnChose.GetComponent<Image>().sprite = btnSelect;
            price.SetActive(false);
            text.SetText("Chose");
        }
        curIndex = 4;
        if (curIndex == indexChose && !isLock[curIndex])
        {
            btnChose.SetActive(false);
        }
        else
        {
            btnChose.SetActive(true);
        }
    }
    public void SetChose()
    {
        btnHero[indexChose].GetComponent<Image>().sprite = bannerUnSelect;
        indexChose = curIndex;
        btnHero[indexChose].GetComponent<Image>().sprite = bannerSelect;
        StaticLobbySend.numHero = indexChose;
        textName.SetText(btnHero[indexChose].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        if (isLock[indexChose])
        {
            isLock[indexChose] = false;
            GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyController>().upDateLock();
        }
        reviewMain.runtimeAnimatorController = animator[indexChose];
        btnChose.SetActive(false);
    }
    public void SetLock(bool[] isLockHero)
    {
        isLock = isLockHero;
    }
    public bool[] GetLock()
    {
        return isLock;
    }
}
