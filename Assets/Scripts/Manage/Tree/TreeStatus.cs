using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus
{
    private int hp, wood;

    public TreeStatus()
    {
        hp = Random.Range(80, 150);
        wood = Random.Range(1, 5);
    }

    public int getHp()
    {
        return hp;
    }
    public int getWood()
    {
        return wood;
    }
}
