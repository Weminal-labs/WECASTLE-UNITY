using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatsForJSON
{
    public string id { get; set; }
    public int type_hero { get; set; }
    public int health { get; set; }
    public int max_health { get; set; }
    public int damage { get; set; }
    public int speed { get; set; }
    public int level { get; set; }
    public int exp { get; set; }
    public int max_exp { get; set; }
    public int location_x { get; set; }
    public int location_y { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public MobStatsForJSON(MobStats mob)
    {
        id = mob.getId();
        type_hero = mob.getMobType();
        health = mob.getHealth();
        max_health = mob.getMaxHealth();
        damage = mob.getDamage();
        speed = mob.getSpeed();
        level = mob.getLevel();
        exp = mob.getExp();
        max_exp = mob.getMaxExp();
        location_x = (int)(mob.getPos()[0]);
        location_y = (int)(mob.getPos()[1]);
        name = mob.getName();
        description = mob.getHistory();
    }
}
