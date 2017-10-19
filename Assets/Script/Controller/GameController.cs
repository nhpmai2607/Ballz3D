using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameState model { get; private set; }
    private GameView view;

    public BallController mainBallController;
    public MapController mapController;
    public PlayerController playerController;
    public SceneController sceneController;

    private Vector3 mainBallPos;
    private Quaternion mainBallRot;

    private void Awake () {
        model = new GameState();
        view = GetComponent<GameView>();
	}

    private void Start()
    {
        mapController.SpawnObstacles();
        view.UpdateView(model.numBalls, model.count);
    }

    private void Update () {
        if (model.isGameOver)
        {
            sceneController.LoadScene("GameOver");
        }

        if (!model.areBouncing)
        {
            mainBallController.Turning();
            mainBallController.RenderDirectionLine();
        }

        CheckFinishBouncing();
    }

    private void FixedUpdate()
    {
        ShootMainBall();

        model.IncreaseShootTime(Time.deltaTime);

        InstantiateAndShootBouncingBall();
    }

    private void OnDestroy()
    {
        //Debug.Log("OnDestroy");
        PlayerPrefs.SetInt("Count", model.count);
        playerController.serializePlayer();
    }

    private void ShootMainBall()
    {
        if (!model.areBouncing && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            model.UpdateBeforeShoot();
            mainBallPos = mainBallController.gameObject.transform.position;
            mainBallRot = mainBallController.gameObject.transform.rotation;
            mainBallController.Bouncing();
        }
    }

    private void InstantiateAndShootBouncingBall()
    {
        // each bouncing ball is shot 0.2 sec after the previous shoot
        if (model.areBouncing && model.bouncingBalls < model.numBalls - 1 && model.shootTime >= model.shootDuration)
        {
            GameObject ball = view.InstantiateBouncingBall(mainBallPos, mainBallRot);
            //Debug.Log("Instantiate " + bouncingBalls);
            ball.GetComponent<BallController>().Bouncing();
            model.UpdateAfterShootBouncingBall();
        }
    }

    private void CheckFinishBouncing()
    {
        if (model.hitGround == model.numBalls)
        {
            model.UpdateAfterFinishBouncing();
            view.UpdateView(model.numBalls, model.count);
            mapController.MoveObstaclesDown();
            mapController.SpawnObstacles();
        }
    }
}
