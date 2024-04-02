using System.Collections;
using System.Collections.Generic;
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
        this.parent.GetComponent<CanUseUnit>().getPlace().GetComponent<UnitChose>().setMobStat(mobStats);
    }
}
