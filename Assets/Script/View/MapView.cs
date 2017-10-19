using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {
    public GameObject blockPrefab;
    public GameObject bonusBallPrefab;
    public GameObject moneyBallPrefab;

    public float mapWidth;
    public float mapHeight;
    public float mapDepth;
    public float obstacleScale;

    public GameObject InstantiateObstacle(Map.Obstacles tag, Vector3 pos)
    {
        switch (tag)
        {
            case Map.Obstacles.BLOCK:
                return Instantiate(blockPrefab, pos, Quaternion.identity, transform);
            case Map.Obstacles.BONUSBALL:
                return Instantiate(bonusBallPrefab, pos, Quaternion.identity, transform);
            case Map.Obstacles.MONEYBALL:
                return Instantiate(moneyBallPrefab, pos, Quaternion.identity, transform);
            default:
                return Instantiate(blockPrefab, pos, Quaternion.identity, transform);
        }
    }

    public void MoveObstacleDown(GameObject obstacle)
    {
        obstacle.transform.position = new Vector3(obstacle.transform.position.x, 
                                                  obstacle.transform.position.y - obstacle.transform.localScale.y, 
                                                  obstacle.transform.position.z);
    }
}
