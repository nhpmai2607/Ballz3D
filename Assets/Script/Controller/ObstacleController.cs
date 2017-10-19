using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleController : MonoBehaviour {

    public abstract void OnTriggerEnterGround(Collider other);

    public abstract void OnTriggerEnter(Collider other);
}
