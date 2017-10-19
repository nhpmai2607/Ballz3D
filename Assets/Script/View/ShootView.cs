using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootView : MonoBehaviour {

    public GameObject ballPrefab;

    public GameObject InstantiateBouncingBall(Vector3 mainBallPos, Quaternion mainBallRot)
    {
        return Instantiate(ballPrefab, mainBallPos, mainBallRot, transform);
    }
}
