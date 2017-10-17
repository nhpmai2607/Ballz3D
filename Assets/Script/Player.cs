using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Player {
    public int count { get; set; }
    public int bestScore { get; set; }
    public int money { get; set; }

    public Player()
    {
        count = 0;
        bestScore = 0;
        money = 0;
    }
}
