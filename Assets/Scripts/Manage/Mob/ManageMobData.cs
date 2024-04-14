using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ManageMobData : MonoBehaviour
{
    private List<MobStats> mobStats;
    private List<MobDataPersistance> dataPersistances;
    [SerializeField]
    private GameObject[] mobPrefabs;
    public static ManageMobData instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Stats Mob Data Manager in this scence.");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataPersistances = FindAllMobData();
        loadMob();
        /*string json = "[{\r\n        id: \"0x38dasjlddanddnalsdjknal\",\r\n        type_hero: 1,\r\n        health: 100,\r\n        max_health: 100, \r\n        damage: 12,\r\n        speed: 10,\r\n        level: 1,\r\n        exp: 0,\r\n        max_exp: 100,\r\n        location_x: 1,\r\n        location_y: 1,\r\n        name: \"daxua\",\r\n        description: \"Char 1\"\r\n    }, \r\n    {\r\n        id: \"0x97andajslddanddnalsdjknal\",\r\n        type_hero: 0,\r\n        health: 120,\r\n        max_health: 120, \r\n        damage: 12,\r\n        speed: 10,\r\n        level: 1,\r\n        exp: 0,\r\n        max_exp: 100,\r\n        location_x: 1,\r\n        location_y: 1,\r\n        name: \"sinobuii\",\r\n        description: \"Char 2\"\r\n    }\r\n    ]";
        loadMobExist(json);*/
    }
    public List<MobStats> getListMob()
    {
        return mobStats;
    }
    public void newMob()
    {
        mobStats = new List<MobStats>();
    }
    public void loadMob()
    {
        if (mobStats == null)
        {
            newMob();
        }
        //load data
        foreach (MobStats mobStats in mobStats)
        {
            foreach (MobDataPersistance data in dataPersistances)
            {
                if (data.CompareData(mobStats.getId()))
                {
                    data.LoadData(mobStats);
                    break;
                }
            }
        }
    }
    public void addMob(MobStats data)
    {
        mobStats.Add(data);
        loadMob();
    }
    public void saveMob()
    {
        List<MobStatsForJSON> jsonList = new List<MobStatsForJSON>();
        foreach (MobStats mob in mobStats)
        {
            foreach (GameObject objMob in GameObject.FindGameObjectsWithTag("Ally"))
            {
                if (objMob.GetComponent<MobStatus>().getIDMob().CompareTo(mob.getId()) == 0)
                {
                    mob.setPos(objMob.transform.position);
                    jsonList.Add(new MobStatsForJSON(mob));
                    break;
                }
            }
        }
        /*        string json = JsonConvert.SerializeObject(jsonList);
                SaveListMob(json);*/
    }
    [DllImport("__Internal")]
    public static extern void SaveMob(string json);

    [DllImport("__Internal")]
    public static extern void SaveListMob(string json);
    private void OnApplicationQuit()
    {
        saveMob();
    }
    public void loadMobExist(string json)
    {
        List<JsonToMob> mobStatsForJSONs = JsonConvert.DeserializeObject<List<JsonToMob>>(json);
        MobStats pointer;
        foreach (JsonToMob mob in mobStatsForJSONs)
        {
            if (mob.health > 0)
            {
                pointer = new MobStats(mob.id, mob.type_hero, mob.max_health, mob.damage, mob.speed, mob.name, mob.description);
                addMob(pointer);
                Instantiate(mobPrefabs[pointer.getMobType()], new Vector3(mob.location_x, mob.location_y), Quaternion.identity).GetComponent<MobStatus>().LoadData(pointer);
            }
        }
    }
    public void LoadNewIdForMob(string id, string fakeId)
    {
        foreach (GameObject mob in GameObject.FindGameObjectsWithTag("Ally"))
        {
            if (mob.GetComponent<MobStatus>().getIDMob().CompareTo(fakeId) == 0)
            {
                mob.GetComponent<MobStatus>().GetMobStats().setNewID(id);
                break;
            }
        }
    }
    private List<MobDataPersistance> FindAllMobData()
    {
        IEnumerable<MobDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>()
            .OfType<MobDataPersistance>();
        return new List<MobDataPersistance>(dataPersistances);
    }
    public void MobInRain()
    {
        foreach (GameObject mob in GameObject.FindGameObjectsWithTag("Ally"))
        {
            mob.GetComponent<MobStatus>().rainWeather();
        }
    }
}
