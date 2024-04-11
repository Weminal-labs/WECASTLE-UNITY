using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class LoadMobInfo : MonoBehaviour
{
    private MobStats m_Stats;
    [SerializeField]
    private Button buttonClose;
    private int unit;
    [SerializeField]
    private Sprite[] unitIcons;
    [SerializeField]
    private GameObject health, damage, speed,maxExp, exp, lv, iconUnit, placeHolderUnit;
    private TextMeshProUGUI healthText, damageText, speedText, expText, lvText;
    [SerializeField]
    private GameObject name, history, placeHolder;
    [SerializeField]
    private RuntimeAnimatorController[] unitAnim;
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
        expText = exp.GetComponent<TextMeshProUGUI>();
        lvText = lv.GetComponent<TextMeshProUGUI>();
        maxExp.GetComponent<Slider>().maxValue = m_Stats.getMaxExp();
        maxExp.GetComponent<Slider>().value = m_Stats.getExp();
        name.GetComponent<TextMeshProUGUI>().SetText(m_Stats.getName().ToString());
        history.GetComponent<TextMeshProUGUI>().SetText(m_Stats.getHistory().ToString());
        healthText.SetText(m_Stats.getHealth().ToString() + "/ " + m_Stats.getMaxHealth().ToString());
        damageText.SetText(m_Stats.getDamage().ToString());
        speedText.SetText(m_Stats.getSpeed().ToString());
        expText.SetText(m_Stats.getExp().ToString() + "/ " + m_Stats.getMaxExp().ToString());
        lvText.SetText(m_Stats.getLevel().ToString());
        iconUnit.GetComponent<Image>().sprite = unitIcons[m_Stats.getMobType()];
        placeHolderUnit.GetComponent<Animator>().runtimeAnimatorController = unitAnim[m_Stats.getMobType()] as RuntimeAnimatorController;
    }
}
