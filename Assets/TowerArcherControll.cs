using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcherControll : MonoBehaviour
{
    [SerializeField]
    private GameObject archerStatue;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<MobInBuilding>().getNumMob() > 0)
        {
            archerStatue.GetComponent<MobStatus>().LoadData(this.GetComponent<MobInBuilding>().getMob(0));
            archerStatue.SetActive(true);
        }
        else
        {
            archerStatue.SetActive(false);
        }
    }
    public void MobLeftBuilding()
    {
        archerStatue.SetActive(false);
    }
}
