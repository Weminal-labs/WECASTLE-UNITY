using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MobInBuilding : MonoBehaviour
{
    [SerializeField]
    private List<MobStats> mob;
    [SerializeField]
    private List<GameObject> gameObjectsMob;
    [SerializeField]
    private int type;
    private void Start()
    {
        mob = new List<MobStats>();
        this.gameObjectsMob = new List<GameObject>();
    }
    public void addMob(MobStats mob)
    {
        if (mob != null)
        {
            this.mob.Add(mob);
        }
    }
    public void removeMob(MobStats mob)
    {
        if (mob != null)
        {
            
            this.mob.Remove(mob);
        }
    }
    public int countMob()
    {
        return this.mob.Count;
    }
    public MobStats getMob(int i)
    {
        return this.mob[i];
    }
    public int returnType()
    {
        return type;
    }
    public int getNumMob()
    {
        return this.mob.Count;
    }
    public void setMob(GameObject mob)
    {
        gameObjectsMob.Add(mob);
    }
    public GameObject getObjMOb(int mob)
    {
        return gameObjectsMob[mob];
    }
    public void removeObjMob(GameObject mob)
    {
        gameObjectsMob.Remove(mob);
    }
    public List<GameObject> getAllGameObjectsMob()
    {
        return gameObjectsMob;
    }
}
