using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    private string idPlayer;
    private string name;
    private int lv, exp, maxExp, curGold, curOre, curWood, curMeat, maxStock;
    public PlayerInfo(string name) 
    {
        this.name = name;
        this.idPlayer = System.Guid.NewGuid().ToString();
        lv = 1;
        exp = 0;
        maxExp = 15;
        curGold = 50;
        curOre = 50;
        curWood = 50;
        curMeat = 50;
    }
    public string getPName()
    {
        return this.name;
    }
    public string getIdPlayer() { return this.idPlayer;}
    public int getLv() { return this.lv;}
    public int getExp() { return this.exp;}
    public void setExp(int exp)
    {
        if (this.exp + exp >= maxExp)
        {
            this.exp = this.exp + exp - maxExp;
            this.lv += 1;
            this.maxExp = this.maxExp + 5 * lv;
        }
        else
        {
            this.exp += exp;
        }
    }
    public int getMaxExp() { return this.maxExp;}
    public int getCurGold() {  return this.curGold;}
    public int getCurOre() {  return this.curOre;}
    public int getCurWood() {  return this.curWood;}
    public int getCurMeat() {  return this.curMeat;}
    public void setCurGold(int gold)
    {
        this.curGold += gold;
    }
    public void setCurOre(int ore)
    {
        this.curOre += ore;
    }
    public void setCurMeat(int meat)
    {
        this.curMeat += meat;
    }
    public void setCurWood(int wood)
    {
        this.curWood += wood;
    }

}