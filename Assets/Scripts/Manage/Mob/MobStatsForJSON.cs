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
    public int location_x { get; set; }
    public int location_y { get; set; }
    public MobStatsForJSON(MobStats mob)
    {
        id = mob.getId();
        type_hero = mob.getMobType();
        health = mob.getHealth();
        max_health = mob.getMaxHealth();
        damage = mob.getDamage();
        speed = mob.getSpeed();
        location_x = (int)(mob.getPos()[0]);
        location_y = (int)(mob.getPos()[1]);
    }
}
