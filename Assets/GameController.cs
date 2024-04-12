using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private string id;
    [SerializeField]
    private float time;
    private PlayerInfo playerInfo;
    [SerializeField]
    private Button logOut;
    public GameObject text;
    [SerializeField]
    private GameObject lv, exp, wood, gold, meat, clock, spawnEnemy;
    [SerializeField]
    private string json;
    // Start is called before the first frame update

    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnLeave;
    [SerializeField] GameObject spawnController;
    float spawnInterval = 330;
    void Start()
    {
        /*RequestAddress();*/
        ReceiveAddress("SSSS");
        if (logOut != null)
        {
            logOut.onClick.AddListener(RequestLogOut);
        }
        
        time = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0.0f)
        {
            time -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(time / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(time % 60);
            string timeCount = string.Format("{0:00}:{1:00}", minutes, seconds);
            clock.GetComponent<TextMeshProUGUI>().SetText(timeCount);
        }
        else
        {
            spawnEnemy.GetComponent<SpawnController>().SpawnEnemy();
            time = 30.0f;
        }
    }

    public void ReceiveAddress(string id)
    {
        this.id = id;
        playerInfo = new PlayerInfo(id);
        PlayerInfoJson info = new PlayerInfoJson(playerInfo);
        Debug.Log(JsonConvert.SerializeObject(info));
        text.GetComponent<TextMeshProUGUI>().SetText(id);
        lv.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getLv().ToString());
        exp.GetComponent<Slider>().maxValue = playerInfo.getMaxExp();
        exp.GetComponent<Slider>().value = playerInfo.getExp();
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
    }
    [DllImport("__Internal")]
    public static extern void RequestLogOut();
    [DllImport("__Internal")]
    public static extern void RequestAddress();
    public void setMaterials(int m, int g, int w)
    {
        playerInfo.setCurMeat(m);
        playerInfo.setCurGold(g);
        playerInfo.setCurWood(w);
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
        int plusEXP = (m + g + w);
        if( plusEXP > 0 )
        {
            playerInfo.setExp(plusEXP);
        }
        lv.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getLv().ToString());
        exp.GetComponent<Slider>().value = playerInfo.getExp();
        exp.GetComponent<Slider>().maxValue = playerInfo.getMaxExp();
        Debug.Log(playerInfo.getExp());
    }
    public PlayerInfo getPlayer()
    {
        return playerInfo;
    }

    public void loseGame()
    {
        playerInfo.setLose();
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
    }
    private float calculatorTime(MobStats mob)
    {
        return 100.0f / (mob.getDamage() / 10.0f);
    }
}
