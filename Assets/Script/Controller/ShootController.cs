using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
    public Shoot model { get; private set; }
    private ShootView view;

    public BallController mainBallController;
    public GameState gameState;

    private Vector3 mainBallPos;
    private Quaternion mainBallRot;

    private void Awake()
    {
        model = new Shoot();
        view = GetComponent<ShootView>();
    }

    private void Start()
    {
        gameState = GameObject.Find("GameController").GetComponent<GameController>().model;
    }

    public void ShootMainBall()
    {
        if (!gameState.areBouncing && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            gameState.UpdateBeforeShoot();
            model.Reset();
            mainBallPos = mainBallController.gameObject.transform.position;
            mainBallRot = mainBallController.gameObject.transform.rotation;
            mainBallController.Bouncing();
        }
    }

    public void InstantiateAndShootBouncingBall()
    {
        // each bouncing ball is shot 0.2 sec after the previous shoot
        if (gameState.areBouncing && 
            model.bouncingBalls < gameState.numBalls - 1 
            && model.shootTime >= model.shootDuration)
        {
            GameObject ball = view.InstantiateBouncingBall(mainBallPos, mainBallRot);
            //Debug.Log("Instantiate " + bouncingBalls);
            ball.GetComponent<BallController>().Bouncing();
            model.UpdateAfterShootBouncingBall();
        }
    }

    public bool CheckFinishShoot()
    {
        if (model.hitGround == gameState.numBalls)
        {
            gameState.UpdateAfterShoot(model.tempBonusBalls);
            model.Reset();
            return true;
        }
        return false;
    }
}
