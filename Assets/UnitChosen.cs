using MBT;
using UnityEngine;
using UnityEngine.UI;

public class UnitChosen : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    private MobStats mobStats;

    public void setParent(GameObject parent, MobStats mob)
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(setMobStat);
        this.parent = parent;
        this.mobStats = mob;
    }

    public void setMobStat()
    {
        foreach (GameObject pointer in GameObject.FindGameObjectsWithTag("Ally"))
        {
            if (pointer.GetComponent<MobStatus>().getIDMob().CompareTo(mobStats.getId()) == 0)
            {
                string id = pointer.GetComponent<MobStatus>().getIDMob();
                foreach (GameObject mob in GameObject.FindGameObjectsWithTag("Ally"))
                {
                    if (id.CompareTo(mob.GetComponent<MobStatus>().getIDMob()) == 0)
                    {
                        this.parent.GetComponent<CanUseUnit>().getPlace().GetComponent<UnitChose>().SaveMobInBuilding(mob);
                        this.parent.SetActive(false);
                        mob.SetActive(false);
                        break;
                    }
                }
            }
        }
        this.parent.GetComponent<CanUseUnit>().getPlace().GetComponent<UnitChose>().setMobStat(mobStats);
    }
}
