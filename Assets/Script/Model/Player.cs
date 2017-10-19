using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Player {
    public int bestScore { get; set; }
    public int money { get; set; }
    public List<int> materials { get; set; }

    public Player()
    {
        bestScore = 0;
        money = 0;
        materials = new List<int>();
        materials.Add(0);
    }
}
