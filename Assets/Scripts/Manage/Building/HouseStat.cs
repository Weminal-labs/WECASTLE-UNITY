using System.Collections;
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
    private string name;
    [SerializeField]
    private GameObject buildingCanvas, buildingIcon, listMob, mob1, mob2, pallette1, hpUi, nameBuilding;
    [SerializeField]
    private GameObject[] fire;
    private int countFire;
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;
    public bool isDoubleClick = false;

    [SerializeField]
    private GameObject destroyHouse;
    private void Start()
    {
        hp = 450;
        maxHp = 450;
        countFire = 0;
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
                nameBuilding.GetComponent<TextMeshProUGUI>().SetText(name);
                mob1.SetActive(true);
                mob2.SetActive(false);
                pallette1.GetComponent<UnitChose>().setBuilding(this.gameObject);
                hpUi.GetComponent<Slider>().maxValue = maxHp;
                hpUi.GetComponent<Slider>().value = hp;
                if (this.gameObject.GetComponent<MobInBuilding>().countMob() > 0)
                {
                    pallette1.GetComponent<UnitChose>().loadMobStat(this.gameObject.GetComponent<MobInBuilding>().getMob(0), 0);
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
    public void TakeDame(int Damage)
    {
        if (hp - Damage < 0)
        {
            DesTroyBuilding();
        }
        else
        {
            hp -= Damage;
        }
        if (hp < 300 && countFire < 3)
        {
            fire[countFire].SetActive(true);
            countFire += 1;
        }
    }
    public void DesTroyBuilding()
    {
        foreach (GameObject mob in this.gameObject.GetComponent<MobInBuilding>().getAllGameObjectsMob())
        {
            mob.SetActive(true);
            mob.GetComponent<MobStatus>().mobOutBuilding();
        }
        if(this.gameObject.GetComponent<MobInBuilding>().getNumMob() > 0)
        {
            if(this.gameObject.GetComponent<MobInBuilding>().returnType() == 1)
            {
                this.gameObject.GetComponent<TowerArcherControll>().MobLeftBuilding();
            }
            this.gameObject.GetComponent<MobInBuilding>().removeMob(this.gameObject.GetComponent<MobInBuilding>().getMob(0));
            this.gameObject.GetComponent<MobInBuilding>().removeObjMob(this.gameObject.GetComponent<MobInBuilding>().getObjMOb(0));
        }
        fire[0].SetActive(false);
        fire[1].SetActive(false);
        fire[2].SetActive(false);
        countFire = 0;
        destroyHouse.SetActive(true);
        destroyHouse.GetComponent<DestroyHouseFix>().ReBackHouse();
        this.gameObject.SetActive(false);

    }
    public void ReBackHouse()
    {
        this.hp = this.maxHp;
    }
}
