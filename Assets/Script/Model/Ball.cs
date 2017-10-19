using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball {
    public float shotForce { get; private set; }
    public float moveSpeed { get; private set; }
    public float stayBlock { get; set; }  // time since entered collision with block
                                                  // avoid bug: stuck on block
    public int shootableMask { get; private set; }
    public bool isBouncing { get; set; }
    public bool onGround { get; set; }

    private const float _range = 100f;
    public float range { get { return _range; } }

    private const float _stayBlockMax = 0.2f;  // maximum duration that a ball stays on a block
    public float stayBlockMax { get { return _stayBlockMax; } }

    public AudioClip bounceSound { get; private set; }

    public Ball(float shotForce, float moveSpeed, int shootableMask, AudioClip bounceSound)
    {
        this.shotForce = shotForce;
        this.moveSpeed = moveSpeed;
        stayBlock = 0f;
        this.shootableMask = shootableMask;
        isBouncing = false;
        onGround = true;
        this.bounceSound = bounceSound;
    }
}
