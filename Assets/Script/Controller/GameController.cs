using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameState model { get; private set; }
    private GameView view;

    public MapController mapController;
    public PlayerController playerController;
    public SceneController sceneController;
    public BallController mainBallController;
    public ShootController shootController;

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
        OnGameOver();

        GetMouseInput();

        OnFinishShoot();
    }

    private void FixedUpdate()
    {
        shootController.ShootMainBall();

        if (model.areBouncing)
        {
            shootController.model.IncreaseShootTime(Time.deltaTime);
        }

        shootController.InstantiateAndShootBouncingBall();
    }

    private void OnDestroy()
    {
        //Debug.Log("OnDestroy");
        PlayerPrefs.SetInt("Count", model.count);
        playerController.serializePlayer();
    }

    private void OnFinishShoot()
    {
        if (shootController.CheckFinishShoot())
        {
            view.UpdateView(model.numBalls, model.count);
            mapController.MoveObstaclesDown();
            mapController.SpawnObstacles();
        }
    }

    private void OnGameOver()
    {
        if (model.isGameOver)
        {
            sceneController.LoadScene("GameOver");
        }
    }

    private void GetMouseInput()
    {
        if (!model.areBouncing)
        {
            mainBallController.Turning();
            mainBallController.RenderDirectionLine();
        }
    }
}
