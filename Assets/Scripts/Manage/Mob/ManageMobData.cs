using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using static UnityEngine.UI.CanvasScaler;

public class ManageMobData : MonoBehaviour
{
    private List<MobStats> mobStats;
    private List<MobDataPersistance> dataPersistances;
    [SerializeField]
    private GameObject[] mobPrefabs;
    public static ManageMobData instance { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Stats Mob Data Manager in this scence.");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataPersistances = FindAllMobData();
        loadMob();
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
        foreach(MobStats mobStats in mobStats)
        {
            foreach(MobDataPersistance data in dataPersistances)
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
        foreach (MobDataPersistance data in dataPersistances)
        {
            foreach (MobStats Stats in mobStats)
            {
                if (data.CompareData(Stats.getId()))
                {
                    
                    break;
                }
            }
        }
        
    }
    private void OnApplicationQuit()
    {
        saveMob();
    }
    public int MaxLV()
    {
        int max = 1;
        /*foreach(MobStats mob in mobStats)
        {
            if(mob.getLevel() > max)
            {
                max = mob.getLevel();
            }
        }*/
        return max;
    }
    public void loadMobExist(string json)
    {
        List<JsonToMob> mobStatsForJSONs = JsonConvert.DeserializeObject<List<JsonToMob>>(json);
        MobStats pointer;
        foreach (JsonToMob mob in mobStatsForJSONs)
        {
            pointer = new MobStats(mob.id, mob.type_hero, mob.max_health, mob.damage, mob.speed, mob.name, mob.history);
            Instantiate(mobPrefabs[pointer.getMobType()], new Vector3(mob.location_x, mob.location_y), Quaternion.identity).GetComponent<MobStatus>().LoadData(pointer);
            mobStats.Add(pointer);
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
        foreach(GameObject mob in GameObject.FindGameObjectsWithTag("Ally"))
        {
            mob.GetComponent<MobStatus>().rainWeather();
        }
    }
}
