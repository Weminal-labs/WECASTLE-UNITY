using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobStats
{
    private string id_mob;
    private int mob_type;
    private int health, maxHealth, damage, speed, level, exp, maxExp;
    private float[] position;
    private string name, history;
    public MobStats() { }
    public MobStats(int mob_type, int minHealth, int maxHealth, int minDamage, int maxDamage, int minSpeed, int maxSpeed, string name, string history)
    {
        this.mob_type = mob_type;
        this.id_mob = "DEMO_00001";
        this.maxHealth = Random.Range(minHealth, maxHealth);
        this.health = this.maxHealth;
        this.damage = Random.Range(minDamage, maxDamage);
        this.speed = Random.Range(minSpeed, maxSpeed);
        this.level = 1;
        this.exp = 0;
        this.maxExp = 5;
        this.name = name;
        this.history = history;
    }
    public MobStats(MobStats mob, Vector3 position)
    {
        this.mob_type = mob.mob_type;
        this.id_mob = mob.id_mob;
        this.health = mob.health;
        this.maxHealth = mob.maxHealth;
        this.damage = mob.damage;
        this.speed = mob.speed;
        this.level = mob.level;
        this.exp = mob.exp;
        this.maxExp = mob.maxExp;
        this.name = mob.name;
        this.history = mob.history;
        this.position = new float[3];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }
    public string getId()
    {
        return id_mob;
    }
    public void setHealt(int health)
    {
        this.health += health;
    }
    private void setHealthLvUp()
    {
        this.health += Random.Range(1, 6);
    }
    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    private void setDamageLvUp()
    {
        this.damage += Random.Range(1, 6);
    }
    public int getDamage()
    {
        return damage;
    }
    public int getSpeed()
    {
        return speed;
    }
    public int getLevel()
    {
        return level;
    }
    public void setExp(int exp)
    {
        if (this.exp + exp > this.maxExp)
        {
            this.exp = this.exp + exp - this.maxExp;
            this.maxExp = level * 5 + this.maxExp;
            level += 1;
            setDamageLvUp();
            setHealthLvUp();
        }
        else
        {
            this.exp += exp;
        }
    }
    public int getExp()
    {
        return exp;
    }
    public int getMaxExp()
    {
        return maxExp;
    }
    public string getName()
    {
        return name;
    }
    public string getHistory()
    {
        return history;
    }
    public void setPos(Vector3 position)
    {
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }
}
