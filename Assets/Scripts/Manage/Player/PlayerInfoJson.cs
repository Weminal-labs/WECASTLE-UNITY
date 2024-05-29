using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoJson
{
    public string idPlayer { get; set; }

    public PlayerInfoJson(PlayerInfo info) 
    { 
        idPlayer = info.getIdPlayer();
    }
}
