using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInBuilding : MonoBehaviour
{
    [SerializeField]
    private List<MobStats> mob;
    [SerializeField]
    private int type;
    private void Start()
    {
        mob = new List<MobStats>();
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
}
