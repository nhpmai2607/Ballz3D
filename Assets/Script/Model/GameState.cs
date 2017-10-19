using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    public int numBalls { get; private set; }  // number of shootable balls
    public int count { get; private set; }  // count shooted balls
    public bool areBouncing { get; private set; }  // check whether player can start shooting
    public int tempBonusBalls { get; private set; }  // number of collected bonus balls in the latest shoot
    public int hitGround { get; private set; }  // number of balls which has hit ground in the latest shoot
    public int bouncingBalls { get; private set; }  // number of balls which has been shooted in the latest shoot
                                                    // do not include the main ball
    public float shootTime { get; private set; }  // time passed since the last bouncing ball is shoot
    public bool isGameOver { get; set; }

    private const float _shootDuration = 0.2f;
    public float shootDuration { get { return _shootDuration; } }

    public GameState()
    {
        Reset();
    }

    public void Reset()
    {
        isGameOver = false;
        numBalls = 1;
        count = 1;
        areBouncing = false;
        ResetForNewShoot();
    }

    private void ResetForNewShoot()
    {
        tempBonusBalls = 0;
        hitGround = 0;
        bouncingBalls = 0;
        shootTime = 0f;
    }

    public void UpdateBeforeShoot()
    {
        areBouncing = true;
        ResetForNewShoot();
    }

    public void UpdateAfterFinishBouncing()
    {
        areBouncing = false;
        count++;
        numBalls += tempBonusBalls;
        ResetForNewShoot();
    }

    public void UpdateAfterShootBouncingBall()
    {
        bouncingBalls++;
        shootTime = 0f;
    }

    public void IncreaseShootTime(float time)
    {
        if (areBouncing)
        {
            shootTime += time;
        }
    }

    public void IncreaseTempBonusBalls()
    {
        tempBonusBalls++;
    }

    public void IncreaseHitGround()
    {
        hitGround++;
    }
}
