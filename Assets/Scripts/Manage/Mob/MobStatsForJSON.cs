using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatsForJSON
{
    public string id_mob { get; set; }
    public int mob_type { get; set; }
    public int health { get; set; }
    public int maxHealth { get; set; }
    public int damage { get; set; }
    public int speed { get; set; }
    public int level { get; set; }
    public int exp { get; set; }
    public int maxExp { get; set; }
    public float[] position { get; set; }
    public string name { get; set; }
    public string history { get; set; }
    public MobStatsForJSON(MobStats mob)
    {
        id_mob = mob.getId();
        mob_type = mob.getMobType();
        health = mob.getHealth();
        maxHealth = mob.getMaxHealth();
        damage = mob.getDamage();
        speed = mob.getSpeed();
        level = mob.getLevel();
        exp = mob.getExp();
        maxExp = mob.getMaxExp();
        position = mob.getPos();
        name = mob.getName();
        history = mob.getHistory();
    }
}
