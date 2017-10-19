using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot {
    public int tempBonusBalls { get; private set; }  // number of collected bonus balls in the latest shoot
    public int hitGround { get; private set; }  // number of balls which has hit ground in the latest shoot
    public int bouncingBalls { get; private set; }  // number of balls which has been shooted in the latest shoot
                                                    // do not include the main ball
    public float shootTime { get; private set; }  // time passed since the last bouncing ball is shoot

    private const float _shootDuration = 0.2f;
    public float shootDuration { get { return _shootDuration; } }

    public Shoot()
    {
        Reset();
    }

    public void Reset()
    {
        tempBonusBalls = 0;
        hitGround = 0;
        bouncingBalls = 0;
        shootTime = 0f;
    }

    public void UpdateAfterShootBouncingBall()
    {
        bouncingBalls++;
        shootTime = 0f;
    }

    public void IncreaseShootTime(float time)
    {
        shootTime += time;
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
