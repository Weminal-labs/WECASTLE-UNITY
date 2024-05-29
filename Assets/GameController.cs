using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private string id;
    private float time;
    private PlayerInfo playerInfo;
    [SerializeField]
    private Button logOut;
    public GameObject text;
    [SerializeField]
    private GameObject wood, gold, meat, clock, wave, spawnEnemy;
    [SerializeField]
    private string json;
    private bool startGame;
    public int waveCount;
    // Start is called before the first frame update

    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnLeave;
    [SerializeField] GameObject spawnController, loadingScreen, loseScreen, winScreen;

    public GameObject tutor;
    public GameObject intro;
    public Button tutorOpen;
    public Button tutorClose;
    float spawnInterval = 330;
    [Header("========Hero=============")]
    [SerializeField]
    private GameObject[] hero;
    public Transform spawnPosition;
    void Start()
    {
        //This is Conection to the server call user data
        /*RequestAddress();*/
        //Intro
        StartCoroutine(stopIntro());
        //Fake data
        string json = "{\r\ngold: 90,\r\nid: \"VN\",\r\nmeat: 90,\r\nwood: 90\r\n}";
        ReceiveAddress(json);
        if (logOut != null)
        {
            logOut.onClick.AddListener(LogOutPrePare);
        }
        if (tutorClose != null)
        {
            tutorClose.onClick.AddListener(closeTutor);
        }
        if (tutorOpen != null)
        {
            tutorOpen.onClick.AddListener(openTutor);
        }
        time = 20.0f;
        startGame = false;
        waveCount = 0;
        MobStats mob;
        MobStats mobSummon;
        switch (StaticLobbySend.numHero)
        {
            case 0:
                mob = new MobStats(3, 3000, 80, 8);
                mobSummon = new MobStats(mob, spawnPosition.position);
                mobSummon.setInBuilding(false);
                Instantiate(hero[StaticLobbySend.numHero], spawnPosition.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(mobSummon);
                break;
            case 1:
                mob = new MobStats(4, 3000, 80, 8);
                mobSummon = new MobStats(mob, spawnPosition.position);
                mobSummon.setInBuilding(false);
                Instantiate(hero[StaticLobbySend.numHero], spawnPosition.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(mobSummon);
                break;
            case 2:
                mob = new MobStats(5, 3000, 80, 8);
                mobSummon = new MobStats(mob, spawnPosition.position);
                mobSummon.setInBuilding(false);
                Instantiate(hero[StaticLobbySend.numHero], spawnPosition.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(mobSummon);
                break;
            case 3:
                mob = new MobStats(6, 3000, 80, 8);
                mobSummon = new MobStats(mob, spawnPosition.position);
                mobSummon.setInBuilding(false);
                Instantiate(hero[StaticLobbySend.numHero], spawnPosition.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(mobSummon);
                break;
            case 4:
                mob = new MobStats(7, 3000, 80, 8);
                mobSummon = new MobStats(mob, spawnPosition.position);
                mobSummon.setInBuilding(false);
                Instantiate(hero[StaticLobbySend.numHero], spawnPosition.position, Quaternion.identity).GetComponent<MobStatus>().LoadData(mobSummon);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
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
                if (waveCount == 6)
                {
                    winGame();
                }
                waveCount += 1;

                OnEnter.Invoke();
                wave.GetComponent<TextMeshProUGUI>().SetText("Wave: " + waveCount);
                spawnEnemy.GetComponent<SpawnController>().SpawnEnemy(waveCount);

                if (waveCount == 5 && StaticLobbySend.numMap != 0)
                {
                    OnEnter.Invoke();
                    wave.GetComponent<TextMeshProUGUI>().SetText("Wave: " + waveCount);
                    spawnEnemy.GetComponent<SpawnController>().SpawnBoss();
                }

                time = 30.0f;
            }
        }
    }

    public void ReceiveAddress(string json)
    {
        Dictionary<string, string> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        this.id = jsonObject["id"].ToString();
        playerInfo = new PlayerInfo(id, Int32.Parse(jsonObject["gold"].ToString()), Int32.Parse(jsonObject["wood"].ToString()), Int32.Parse(jsonObject["meat"].ToString()));
        PlayerInfoJson info = new PlayerInfoJson(playerInfo);
        Debug.Log(JsonConvert.SerializeObject(info));
        text.GetComponent<TextMeshProUGUI>().SetText(id);
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
    }
    public void LogOutPrePare()
    {
        GameObject.FindGameObjectWithTag("MOBDATA").GetComponent<ManageMobData>().saveMob();
        PlayerInfoJson player = new PlayerInfoJson(playerInfo);
    }
    public void setMaterials(int m, int g, int w)
    {
        playerInfo.setCurMeat(m);
        playerInfo.setCurGold(g);
        playerInfo.setCurWood(w);
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
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
        GameObject.FindGameObjectWithTag("MOBDATA").GetComponent<ManageMobData>().saveMob();
        PlayerInfoJson player = new PlayerInfoJson(playerInfo);
        loseScreen.SetActive(true);
    }

    public void winGame()
    {
        winScreen.SetActive(true);
    }

    public void savePlayerData()
    {
        /*PlayerInfoJson player = new PlayerInfoJson(playerInfo);
        SavePlayer(JsonConvert.SerializeObject(player));*/
    }
    private float calculatorTime(MobStats mob)
    {
        return 100.0f / (mob.getDamage() / 10.0f);
    }
    public void stopLoadingScreen()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
            startGame = true;
        }
    }
    public void openTutor()
    {
        tutor.SetActive(true);
    }
    public void closeTutor()
    {
        tutor.SetActive(false);
    }
    IEnumerator stopIntro()
    {
        yield return new WaitForSeconds(11.5f);
        intro.SetActive(false);
        tutor.SetActive(true);
        startGame = true;
    }
}
