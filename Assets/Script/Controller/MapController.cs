using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public Map model { get; private set; }
    private MapView view;

    private bool isFirst;

    private void Awake()
    {
        view = GetComponent<MapView>();
        model = new Map(view.mapWidth, view.mapHeight, view.mapDepth, view.obstacleScale);

        isFirst = true;
    }

    private void RemoveBlock(GameObject block)
    {
        BlockController blockController = block.GetComponent<BlockController>();
        blockController.DecreaseHealth();

        if (!blockController.IsAlive())
        {
            RemoveAndDestroyObstacle(block);
        }
    }

    public void RemoveObstacle(Map.Obstacles tag, GameObject obstacle)
    {
        switch (tag)
        {
            case Map.Obstacles.BLOCK:
                RemoveBlock(obstacle);
                break;
            default:
                RemoveAndDestroyObstacle(obstacle);
                break;
        }
    }

    private void RemoveAndDestroyObstacle(GameObject obstacle)
    {
        model.obstacles.Remove(obstacle);
        Destroy(obstacle);
    }

    public void SpawnObstacles()
    {
        List<Vector3> existingPos = new List<Vector3>();
        SpawnBlocks(existingPos);
        SpawnBonusBall(existingPos);
        SpawnMoneyBall(existingPos);
    }

    private void SpawnBlocks(List<Vector3> existingPos)
    {
        int numBlocks = Random.Range(model.lowNumBlocksSpawned, model.upNumBlocksSpawned);

        for (int i = 0; i < numBlocks; i++)
        {
            SpawnObstacle(Map.Obstacles.BLOCK, existingPos);
        }
    }

    private void SpawnObstacle(Map.Obstacles tag, List<Vector3> existingPos)
    {
        Vector3 newPos = GetNewPos(existingPos);
        existingPos.Add(newPos);

        GameObject newObstacle = view.InstantiateObstacle(tag, newPos);
        model.obstacles.Add(newObstacle);
    }

    private void SpawnBonusBall(List<Vector3> existingPos)
    {
        if (!isFirst)
        {
            SpawnObstacle(Map.Obstacles.BONUSBALL, existingPos);
        }
        isFirst = false;
    }

    private void SpawnMoneyBall(List<Vector3> existingPos)
    {
        int moneyChance = Random.Range(model.lowMoneyChance, model.upMoneyChance);
        if (moneyChance == model.lowMoneyChance)
        {
            SpawnObstacle(Map.Obstacles.MONEYBALL, existingPos);
        }
    }

    public void MoveObstaclesDown()
    {
        foreach (GameObject obstacle in model.obstacles)
        {
            view.MoveObstacleDown(obstacle);
        }
    }

    private Vector3 GetNewPos(List<Vector3> existingPos)
    {
        Vector3 newPos;
        do
        {
            newPos = new Vector3(RandomXZ(model.mapWidth, model.obstacleScale), 
                                model.spawnY, 
                                RandomXZ(model.mapDepth, model.obstacleScale));
        } while (existingPos.Contains(newPos));

        return newPos;
    }

    private float RandomXZ(float size, float step)
    {
        int numPoints = Mathf.FloorToInt(size / step);
        float tempValue = Random.Range(0, numPoints) - (numPoints - 1) / 2f;
        return tempValue * step;
    }

}
