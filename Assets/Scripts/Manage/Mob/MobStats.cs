using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MobStats
{
    private string id_mob { get; set; }
    private int mob_type { get; set; }
    private int health { get; set; }
    private int maxHealth { get; set; }
    private int damage { get; set; }
    private int speed { get; set; }
    private float[] position { get; set; }
    private bool inBuilding { get; set; }
    public MobStats() { }
    public MobStats(int mob_type, int health, int damage, int speed)
    {
        this.mob_type = mob_type;
        this.id_mob = generateID();
        this.maxHealth = health;
        this.health = this.maxHealth;
        this.damage = damage;
        this.speed = speed;
        this.inBuilding = false;
    }
    public MobStats(string id, int mob_type, int maxHealth, int Damage, int Speed)
    {
        this.id_mob = id;
        this.mob_type = mob_type;
        this.maxHealth = maxHealth;
        this.health = this.maxHealth;
        this.damage = Damage;
        this.speed = Speed;
        this.inBuilding = false;
    }
    public MobStats(MobStats mob, Vector3 position)
    {
        this.mob_type = mob.mob_type;
        this.id_mob = mob.id_mob;
        this.health = mob.health;
        this.maxHealth = mob.maxHealth;
        this.damage = mob.damage;
        this.speed = mob.speed;
        this.inBuilding = false;
        this.position = new float[3];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }
    public int getMobType()
    {
        return this.mob_type;
    }
    public string getId()
    {
        return id_mob;
    }
    public void setHealth(int health)
    {
        this.health += health;
        if(this.health < 0)
        {
            this.health = 0;
        }
    }
    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int getDamage()
    {
        return damage;
    }
    public int getSpeed()
    {
        return speed;
    }
    public void setPos(Vector3 pos)
    {
        this.position[0] = pos.x;
        this.position[1] = pos.y;
        this.position[2] = pos.z;
    }
    public float[] getPos()
    {
        return this.position;
    }

    private string generateID()
    {
        return new string(System.Guid.NewGuid().ToString());
    }

    public void setInBuilding(bool inbuilding)
    {
        this.inBuilding = inbuilding;
    }
    public bool isInBuilding()
    {
        return this.inBuilding;
    }
    public void setNewID(string id)
    {
        this.id_mob = id;
    }
}
