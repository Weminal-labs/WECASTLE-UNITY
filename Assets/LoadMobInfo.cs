using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMobInfo : MonoBehaviour
{
    private MobStats m_Stats;
    [SerializeField]
    private Button buttonClose;
    private int unit;
    [SerializeField]
    private Sprite[] unitIcons;
    [SerializeField]
    private GameObject health, damage, speed, name, iconUnit, placeHolderUnit;
    private TextMeshProUGUI healthText, damageText, speedText, nameText, history;
    [SerializeField]
    private GameObject  placeHolder;
    [SerializeField]
    private RuntimeAnimatorController[] unitAnim;
    [Header("===========NameMob============")]
    public string[] nameMob;
    // Start is called before the first frame update
    void Start()
    {
        if (buttonClose != null)
        {
            buttonClose.onClick.AddListener(Close);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Close()
    {
        placeHolder.SetActive(false);
        var tempColor = this.gameObject.GetComponent<RawImage>().color;
        tempColor.a = 0f;
        this.gameObject.GetComponent<RawImage>().color = tempColor;
    }

    public void LoadData(MobStats data)
    {
        m_Stats = data;
    }

    public void OpenMenu()
    {
        placeHolder.SetActive(true);
        var tempColor = this.gameObject.GetComponent<RawImage>().color;
        tempColor.a = 100f;
        this.gameObject.GetComponent<RawImage>().color = tempColor;
        unit = 0;
        healthText = health.GetComponent<TextMeshProUGUI>();
        damageText = damage.GetComponent<TextMeshProUGUI>();
        speedText = speed.GetComponent<TextMeshProUGUI>();
        nameText = name.GetComponent<TextMeshProUGUI>();
        healthText.SetText(m_Stats.getHealth().ToString() + "/ " + m_Stats.getMaxHealth().ToString());
        damageText.SetText(m_Stats.getDamage().ToString());
        speedText.SetText(m_Stats.getSpeed().ToString());
        iconUnit.GetComponent<Image>().sprite = unitIcons[m_Stats.getMobType()];
        placeHolderUnit.GetComponent<Animator>().runtimeAnimatorController = unitAnim[m_Stats.getMobType()] as RuntimeAnimatorController;
        nameText.SetText(nameMob[m_Stats.getMobType()]);
    }
}
