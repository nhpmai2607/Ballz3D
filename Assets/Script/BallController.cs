﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float shotForce;
    public float moveSpeed;
    public float range = 100f;
    public AudioClip bounceSound;

    AudioSource audioSource;
    int shootableMask;
    public bool isBouncing = false;
    public bool isMain = false;
    bool onGround = true;
    LineRenderer directionLine;
    Rigidbody rb;
    BlocksController spawn;
    BallsController ballsController;
    float stayBlock;

    void Awake()
    {
        onGround = true;
        shootableMask = LayerMask.GetMask("Shootable");
        directionLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //Debug.Log(LayerMask.NameToLayer("Ball"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("Ball"));
    }

    private void Start()
    {
        spawn = GameObject.Find("BlocksController").GetComponent<BlocksController>();
        ballsController = GameObject.Find("BallsController").GetComponent<BallsController>();
    }

    private void FixedUpdate()
    {
        if (rb.IsSleeping() && isBouncing)
        {
            rb.WakeUp();
        }
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
            directionLine.enabled = false;
        }
    }
	
    public void Bouncing()
    {
        onGround = false;
        //Debug.Log("In Bouncing");
        directionLine.enabled = false;
        rb.AddForce(transform.forward * Time.deltaTime * shotForce);
        //Debug.Log("AddForce");
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
                //Debug.Log("Collision Enter");
                //Debug.Log("position " + transform.position.x + " " + transform.position.y + " " + 
                //transform.position.z + " isBouncing " + isBouncing + " onGround " + onGround);
                //if (!isMain)
                //{
                    //Debug.Log("Enter ground: is Bouncing: " + isBouncing + " onGround: " + onGround);
                //}
                if (isBouncing && !onGround)
                {
                    ballsController.hitGround += 1;
                    rb.velocity = Vector3.zero;
                    if (!isMain)
                    {
                        //Debug.Log("destroyyyy " + ballsController.hitGround);
                        //Debug.Log(transform.position);
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
                stayBlock = 0f;
                playEffectSound();
                spawn.removeBlock(collision.gameObject);
                break;
            case "Boundary":
                playEffectSound();
                break;
        }
    }

    private void playEffectSound()
    {
        audioSource.volume = PlayerPrefs.HasKey("EffectSound") ? PlayerPrefs.GetFloat("EffectSound") : 1f;
        audioSource.mute = PlayerPrefs.HasKey("EffectSoundMute") && PlayerPrefs.GetInt("EffectSoundMute") == 1 ? true : false;
        audioSource.PlayOneShot(bounceSound);
    }

    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Block":
                stayBlock += Time.deltaTime;
                if (stayBlock >= 200)
                {
                    //Debug.Log("Collision stay");
                    spawn.removeBlock(collision.gameObject);
                }
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                if (!isMain)
                {
                    //Debug.Log("Exit Ground");
                }
                isBouncing = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BonusBall":
                //Debug.Log("Trigger in ball");
                ballsController.tempBonusBalls += 1;
                spawn.removeBonusBall(other.gameObject);
                break;
        }
    }
}
