using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoJson
{
    public string idPlayer { get; set; }
    public int lv { get; set; }
    public int exp { get; set; }
    public int maxExp { get; set; }
    public int curGold { get; set; }
    public int curWood { get; set; }
    public int curMeat { get; set; }

    public PlayerInfoJson(PlayerInfo info) 
    { 
        idPlayer = info.getIdPlayer();
        lv = info.getLv();
        exp = info.getExp();
        maxExp = info.getMaxExp();
        curGold = info.getCurGold();
        curMeat = info.getCurMeat();
        curWood = info.getCurWood();
    }
}
