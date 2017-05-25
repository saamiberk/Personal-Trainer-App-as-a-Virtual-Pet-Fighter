using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerInfo
{
    public static PlayerInfo current;
    public string playerName;
    public bool isSleeping;

    public PlayerInfo()
    {
        playerName = "";
        isSleeping = false;
    }
}