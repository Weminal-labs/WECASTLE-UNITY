using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MIneGold : MonoBehaviour
{
    [SerializeField]
    private float time;
    private bool inMine;
    private int countMob;
    [SerializeField]
    private GameObject textTime, gameController;
    // Start is called before the first frame update
    void Start()
    {
        inMine = false;
        time = 0.0f;
        countMob = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<MobInBuilding>().getNumMob() != countMob)
        {
            countMob = this.gameObject.GetComponent<MobInBuilding>().getNumMob();
            if(countMob == 1)
            {
                time = calculatorTime(this.gameObject.GetComponent<MobInBuilding>().getMob(0));
                textTime.SetActive(true);
                inMine = true;
            }
            else
            {
                textTime.SetActive(false);
                inMine = false;
            }
        }
        if (time>0.0f)
        {
            time -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(time / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(time % 60);
            string timeCount = string.Format("{0:00}:{1:00}",minutes,seconds);
            textTime.GetComponent<TextMeshProUGUI>().SetText(timeCount);
        }
        else
        {
            if (inMine)
            {
                gameController.GetComponent<GameController>().setMaterials(0,Random.Range(3,8),0);
                time = calculatorTime(this.gameObject.GetComponent<MobInBuilding>().getMob(0));
                textTime.SetActive(true);
                inMine = true;
            }
        }
    }

    private float calculatorTime(MobStats mob)
    {
        return 100.0f / (mob.getDamage() / 10.0f);
    }
}
