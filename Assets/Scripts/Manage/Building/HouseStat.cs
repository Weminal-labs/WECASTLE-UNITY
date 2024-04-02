using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseStat : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int hp, maxHp;
    [SerializeField]
    private GameObject buildingCanvas, buildingIcon, listMob, mob1, mob2, pallette1, hpUi;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;
    private void Start()
    {
        hp = 100;
        maxHp = 100;
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
                buildingCanvas.SetActive(true);
                buildingIcon.GetComponent<Image>().sprite = icon;
                buildingIcon.GetComponent<Image>().SetNativeSize();
                mob1.SetActive(true);
                mob2.SetActive(false);
                pallette1.GetComponent<UnitChose>().setBuilding(this.gameObject);
                hpUi.GetComponent<Slider>().maxValue = maxHp;
                hpUi.GetComponent<Slider>().value = hp;
                if (this.gameObject.GetComponent<MobInBuilding>().countMob() > 0)
                {
                    pallette1.GetComponent<UnitChose>().loadMobStat(this.gameObject.GetComponent<MobInBuilding>().getMob(0));
                }
                else
                {
                    pallette1.GetComponent<UnitChose>().loadNullMob();
                }
                hpUi.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(hp.ToString() + "/" + maxHp.ToString());
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }
}
