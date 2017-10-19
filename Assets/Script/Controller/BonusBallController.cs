using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallController : ObstacleController {
    MapController mapController;

    private void Start()
    {
        mapController = GameObject.Find("MapController").GetComponent<MapController>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Ground":
                OnTriggerEnterGround(other);
                break;
        }
    }

    public override void OnTriggerEnterGround(Collider other)
    {
        mapController.RemoveObstacle(Map.Obstacles.BONUSBALL, gameObject);
    }
}
