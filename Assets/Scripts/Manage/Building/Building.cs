using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    private string id;
    private int hp, maxHp, type;
    private MobStats mob1, mob2;
    
    private Building(string id, int hp, int maxHp, int type)
    {
        this.id = id;
        this.hp = hp;
        this.maxHp = maxHp;
        this.type = type;
    }
    private Building(string id, int hp, int maxHp, int type, MobStats mob1)
    {
        this.id = id;
        this.hp = hp;
        this.maxHp = maxHp;
        this.type = type;
        this.mob1 = mob1;
    }
    private Building(string id, int hp, int maxHp, int type, MobStats mob1, MobStats mob2)
    {
        this.id = id;
        this.hp = hp;
        this.maxHp = maxHp;
        this.type = type;
        this.mob1 = mob1;
        this.mob2 = mob2;
    }

    public string getId()
    {
        return this.id;
    }
    public int getHp() { return this.hp;}
    public int getMaxHp() { return this.maxHp;}
    public int getType() { return this.type;}
    public MobStats getMob1() {  return this.mob1; }
    public void setMob1(MobStats mob)
    {
        this.mob1 = mob;
    }
    public MobStats getMob2() {  return this.mob2; }
    public void setMob2(MobStats mob)
    {
        this.mob2 = mob;
    }
}
