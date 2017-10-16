using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float shotForce;
    public float moveSpeed;
    public float range = 100f;

    int shootableMask;
    public bool isBouncing = false;
    public bool isMain = false;
    bool onGround = false;
    LineRenderer directionLine;
    Rigidbody rb;
    BlocksController spawn;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        directionLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        spawn = GameObject.Find("BlocksController").GetComponent<BlocksController>();
    }

    public void RenderDirectionLine()
    {
        directionLine.enabled = true;
        directionLine.SetPosition(0, transform.position);
        Ray shootRay = new Ray();
        RaycastHit shootHit;

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            directionLine.SetPosition(1, shootHit.point);
        }
        else
        {
            directionLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
	
    public void Bouncing()
    {
        onGround = false;
        Debug.Log("In Bouncing");
        directionLine.enabled = false;
        rb.AddForce(transform.forward * Time.deltaTime * shotForce);
        Debug.Log("AddForce");
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit wallHit;

        if (Physics.Raycast(camRay, out wallHit, range, shootableMask))
        {
            Vector3 playerToMouse = wallHit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
//        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Ground":
                //Debug.Log("In ball collision Ground before " + rb.velocity + transform.position + isBouncing);
                Debug.Log("Collision Enter");
                Debug.Log("position " + transform.position.x + " " + transform.position.y + " " + 
                    transform.position.z + " isBouncing " + isBouncing + " onGround " + onGround);
                if (isBouncing && !onGround)
                {
                    GameObject.Find("BallsController").GetComponent<BallsController>().hitGround += 1;
                    rb.velocity = Vector3.zero;
                    if (!isMain)
                    {
                        Debug.Log("destroyyyy " + GameObject.Find("BallsController").GetComponent<BallsController>().hitGround);
                        Debug.Log(transform.position);
                        Destroy(this.gameObject);
                    }
                    //transform.position = new Vector3(transform.position.x, 0.025f, transform.position.z);
                    isBouncing = false;
                    onGround = true;
                }
                //                Debug.Log("Ground after " + rb.velocity);
                break;
            case "Block":
                //  Debug.Log("In Ball Collide block");
                spawn.removeBlock(collision.gameObject);
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                Debug.Log("Collision Exit");
                isBouncing = true;
                break;
        }
    }
}
