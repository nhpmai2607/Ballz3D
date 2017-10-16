using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Score {
    public int count { get; set; }
    public int highestScore { get; set; }

    public Score()
    {
        count = 0;
        highestScore = 0;
    }
}
