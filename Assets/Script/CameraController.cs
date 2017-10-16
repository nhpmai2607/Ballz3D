using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float rotSpeed;

    Vector3 target;

	// Use this for initialization
	void Start () {
        target = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(target, -Vector3.up, rotSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(target, Vector3.up, rotSpeed * Time.deltaTime);
        }
    }
}
