using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonForAccount
{
    public string id;
    public int coin;
    public bool[] isLock;
    public JsonForAccount(string id, int coin, bool[] isLock)
    {
        this.id = id;
        this.coin = coin;
        this.isLock = isLock;
    }
}
