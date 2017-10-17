using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsController : MonoBehaviour {
    List<BallController> ballControllers;
    public Text numBallsText;
    public bool areBouncing;
    int numBalls;
    public int tempBonusBalls;
    public int hitGround;
    public int bouncingBalls;
    public GameObject ballPrefab;
    public GameObject mainBall;
    BallController mainBallController;
    float shootTime;
    Vector3 mainBallPos;
    Quaternion mainBallRot;
    BlocksController spawn;
    public CountController countController;

    

    // Use this for initialization
    void Start () {
        mainBallController = mainBall.GetComponent<BallController>();
        spawn = GameObject.Find("BlocksController").GetComponent<BlocksController>();
        areBouncing = false;
        countController.score.count = 0;
        countController.increaseCount();
        numBalls = 1;
        setNumBallsText();
    }

    void setNumBallsText()
    {
        numBallsText.text = "x" + numBalls;
    }

    void InstatiateBalls()
    {
        ballControllers = new List<BallController>();
        for (int i = 0; i < numBalls - 1; i++)
        {
            GameObject ball = Instantiate(ballPrefab, mainBall.transform.position, mainBall.transform.rotation);
            ballControllers.Add(ball.GetComponent<BallController>());
        }
    }

    private void Update()
    {
        if (!areBouncing)
        {
            mainBallController.Turning();
            mainBallController.RenderDirectionLine();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!areBouncing && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            areBouncing = true;
            bouncingBalls = 0;
            hitGround = 0;
            tempBonusBalls = 0;
            //InstatiateBalls();
            mainBallPos = mainBall.transform.position;
            mainBallRot = mainBall.transform.rotation;
            mainBallController.Bouncing();
            shootTime = 0;
        }

        if (areBouncing)
        {
            shootTime += Time.deltaTime;
        }
        if (areBouncing && bouncingBalls < numBalls - 1 && shootTime >= 0.2)
        {
            GameObject ball = Instantiate(ballPrefab, mainBallPos, mainBallRot, transform);
            //Debug.Log("Instantiate " + bouncingBalls);
            ball.GetComponent<BallController>().Bouncing();
            bouncingBalls++;
            shootTime = 0;
        }
        
        if (hitGround == numBalls)
        {
            areBouncing = false;
            countController.increaseCount();
            spawn.getBlockDown();
            spawn.spawnBlock();
            hitGround = 0;
            numBalls += tempBonusBalls;
            setNumBallsText();
            tempBonusBalls = 0;
        }
    }
}
