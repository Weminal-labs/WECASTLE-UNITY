using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    private string idPlayer { get; set; }
    private int curGold { get; set; }
    private int curWood { get; set; }
    private int curMeat { get; set; }
    public PlayerInfo(string idPlayer, int curGold, int curWood, int curMeat) 
    {
        this.idPlayer = idPlayer;
        this.curGold = curGold;
        this.curWood = curMeat;
        this.curMeat = curWood;
    }
    public string getIdPlayer() { return this.idPlayer;}
    public int getCurGold() {  return this.curGold;}
    public int getCurWood() {  return this.curWood;}
    public int getCurMeat() {  return this.curMeat;}
    public void setCurGold(int gold)
    {
        this.curGold += gold;
    }
    public void setCurMeat(int meat)
    {
        this.curMeat += meat;
    }
    public void setCurWood(int wood)
    {
        this.curWood += wood;
    }
    public void setLose()
    {
        this.curGold = 0;
        this.curWood = 0;
        this.curMeat = 0;
    }
}
