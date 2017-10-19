using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float shotForce;
    public float moveSpeed;
    public bool isMain = false;

    public Ball model { get; private set; }
    private BallView view;
    private Rigidbody rb;

    MapController mapController;
    GameController gameController;
    PlayerController playerController;

    void Awake()
    {
        view = GetComponent<BallView>();
        model = new Ball(shotForce, moveSpeed, LayerMask.GetMask("Shootable"), view.bounceSound);
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mapController = GameObject.Find("MapController").GetComponent<MapController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.Find("CustomCamera").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (model.isBouncing && rb.IsSleeping())
        {
            rb.WakeUp();
        }
    }

    public void RenderDirectionLine()
    {
        Ray shootRay = new Ray();
        RaycastHit shootHit;

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, model.range, model.shootableMask))
        {
            view.RenderDirectionLine(transform.position, shootHit.point);
        }
    }
	
    public void Bouncing()
    {
        model.onGround = false;
        //Debug.Log("In Bouncing");
        rb.AddForce(transform.forward * Time.deltaTime * model.shotForce);
        view.Bouncing();
        //Debug.Log("AddForce");
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit wallHit;

        if (Physics.Raycast(camRay, out wallHit, model.range, model.shootableMask))
        {
            Vector3 playerToMouse = wallHit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
        }
    }

    private void OnCollisionEnterGround(Collision collision)
    {
        if (model.isBouncing && !model.onGround)
        {
            gameController.model.IncreaseHitGround();
            rb.velocity = Vector3.zero;
            if (!isMain)
            {
                //Debug.Log("destroyyyy " + ballsController.hitGround);
                //Debug.Log(transform.position);
                Destroy(gameObject);
            }
            //transform.position = new Vector3(transform.position.x, 0.025f, transform.position.z);
            model.isBouncing = false;
            model.onGround = true;
        }
    } 

    private void OnCollisionEnterBlock(Collision collision)
    {
        model.stayBlock = 0f;
        view.PlayEffectSound();
        mapController.RemoveObstacle(Map.Obstacles.BLOCK, collision.gameObject);
    }

    private void OnCollisionEnterBoundary(Collision collision)
    {
        view.PlayEffectSound();
    }

    private void OnCollisionEnter(Collision collision)
    {
//        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Ground":
                //Debug.Log("In ball collision Ground before " + rb.velocity + transform.position + isBouncing);
                //Debug.Log("Collision Enter");
                //Debug.Log("position " + transform.position.x + " " + transform.position.y + " " + 
                //transform.position.z + " isBouncing " + isBouncing + " onGround " + onGround);
                //if (!isMain)
                //{
                //Debug.Log("Enter ground: is Bouncing: " + isBouncing + " onGround: " + onGround);
                //}
                OnCollisionEnterGround(collision);
                //                Debug.Log("Ground after " + rb.velocity);
                break;
            case "Block":
                //  Debug.Log("In Ball Collide block");
                OnCollisionEnterBlock(collision);
                break;
            case "Boundary":
                OnCollisionEnterBoundary(collision);
                break;
        }
    }

    private void OnCollisionStayBlock(Collision collision)
    {
        model.stayBlock += Time.deltaTime;
        if (model.stayBlock >= model.stayBlockMax)
        {
            //Debug.Log("Collision stay");
            mapController.RemoveObstacle(Map.Obstacles.BLOCK, collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Block":
                OnCollisionStayBlock(collision);
                break;
        }
    }

    private void OnCollisionExitGround(Collision collision)
    {
        //if (!isMain)
        //{
            //Debug.Log("Exit Ground");
        //}
        model.isBouncing = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                OnCollisionExitGround(collision);
                break;
        }
    }

    private void OnTriggerEnterBonusBall(Collider other)
    {
        gameController.model.IncreaseTempBonusBalls();
        mapController.RemoveObstacle(Map.Obstacles.BONUSBALL, other.gameObject);
    }

    private void OnTriggerEnterMoneyBall(Collider other)
    {
        playerController.increaseMoney();
        mapController.RemoveObstacle(Map.Obstacles.MONEYBALL, other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BonusBall":
                //Debug.Log("Trigger in ball");
                OnTriggerEnterBonusBall(other);
                break;
            case "MoneyBall":
                OnTriggerEnterMoneyBall(other);
                break;
        }
    }
}
