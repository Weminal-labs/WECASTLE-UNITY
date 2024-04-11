using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class ManageMobData : MonoBehaviour
{
    private List<MobStats> mobStats;
    private List<MobDataPersistance> dataPersistances;
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
    private List<MobDataPersistance> FindAllMobData()
    {
        IEnumerable<MobDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>()
            .OfType<MobDataPersistance>();
        return new List<MobDataPersistance>(dataPersistances);
    }
}
