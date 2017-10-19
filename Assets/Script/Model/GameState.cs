using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    public bool areBouncing { get; private set; }  // check whether player can start shooting
    public int numBalls { get; private set; }  // number of shootable balls
    public int count { get; private set; }  // count shooted balls
    public bool isGameOver { get; set; }

    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        areBouncing = false;
        isGameOver = false;
        numBalls = 1;
        count = 1;
    }

    public void UpdateBeforeShoot()
    {
        areBouncing = true;
    }

    public void UpdateAfterShoot(int tempBonusBalls)
    {
        areBouncing = false;
        count++;
        numBalls += tempBonusBalls;
    }
}
