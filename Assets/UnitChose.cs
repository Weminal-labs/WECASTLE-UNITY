using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitChose : MonoBehaviour
{
    [SerializeField]
    private GameObject container, buildingObj;
    [SerializeField]
    private Button buttonChose, buttonMinus;
    private MobStats mobStats;
    private GameObject mobObj;
    [SerializeField]
    private RuntimeAnimatorController[] animMob;
    [SerializeField]
    private GameObject textTime;
    // Update is called once per frame
    void Start()
    {
        if (buttonChose != null)
        {
            buttonChose.onClick.AddListener(loadListMob);
        }
        if (buttonMinus != null)
        {
            buttonMinus.onClick.AddListener(minusMob);
        }
    }
    public GameObject getBuilding()
    {
        return this.buildingObj;
    }

    private void loadListMob()
    {
        container.SetActive(true);
        container.GetComponent<CanUseUnit>().loadMobCanUse(buildingObj.GetComponent<MobInBuilding>().returnType());
        container.GetComponent<CanUseUnit>().setPlaceHolder(this.gameObject);
    }
    private void minusMob()
    {
        this.buildingObj.GetComponent<MobInBuilding>().removeMob(mobStats);
        this.buildingObj.GetComponent<MobInBuilding>().removeObjMob(mobObj);
        this.mobObj.SetActive(true);
        this.mobObj = null;
        mobStats.setInBuilding(false);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(6).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(false);
    }
    public void setBuilding(GameObject building)
    {
        this.buildingObj = building;
        if (this.buildingObj.GetComponent<MobInBuilding>().returnType() == 2)
        {
            textTime.SetActive(true);
        }
    }
    public void setMobStat(MobStats mob)
    {
        container.GetComponent<CanUseUnit>().loadMobCanUse(buildingObj.GetComponent<MobInBuilding>().returnType());
        this.mobStats = mob;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(7).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getHealth().ToString() + "/" + mob.getMaxHealth().ToString());
        this.gameObject.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getDamage().ToString());
        this.gameObject.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getSpeed().ToString());
        this.gameObject.transform.GetChild(6).GetComponent<Animator>().runtimeAnimatorController = animMob[mob.getMobType()] as RuntimeAnimatorController;
        this.buildingObj.GetComponent<MobInBuilding>().addMob(mob);
        mob.setInBuilding(true);
    }

    public void loadMobStat(MobStats mob, int num)
    {
        container.GetComponent<CanUseUnit>().loadMobCanUse(buildingObj.GetComponent<MobInBuilding>().returnType());
        this.mobStats = mob;
        this.mobObj = buildingObj.GetComponent<MobInBuilding>().getObjMOb(num);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(7).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getHealth().ToString() + "/" + mob.getMaxHealth().ToString());
        this.gameObject.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getDamage().ToString());
        this.gameObject.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(mob.getSpeed().ToString());
        this.gameObject.transform.GetChild(6).GetComponent<Animator>().runtimeAnimatorController = animMob[mob.getMobType()] as RuntimeAnimatorController;
        mob.setInBuilding(true);
    }

    public void loadNullMob()
    {
        mobStats = null;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(6).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(false);
    }

    public void SaveMobInBuilding(GameObject mob)
    {
        this.mobObj = mob;
        this.buildingObj.GetComponent<MobInBuilding>().setMob(mob);
    }
}
