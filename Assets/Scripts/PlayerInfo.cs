using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo
{
    public string playerName;
    public int playerScore;
    
    public PlayerInfo()
    {
        playerName = DatabaseManagement.name;
        playerScore = DatabaseManagement.score;
    }
}
