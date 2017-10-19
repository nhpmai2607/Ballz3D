using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    public List<GameObject> obstacles { get; private set; }
    public enum Obstacles { BLOCK, BONUSBALL, MONEYBALL };

    private const int _lowNumBlocksSpawned = 1;
    public int lowNumBlocksSpawned { get { return _lowNumBlocksSpawned; } }

    private const int _upNumBlocksSpawned = 4;
    public int upNumBlocksSpawned { get { return _upNumBlocksSpawned; } }

    private const int _lowMoneyChance = 0;
    public int lowMoneyChance { get { return _lowMoneyChance; } }

    private const int _upMoneyChance = 5;
    public int upMoneyChance { get { return _upMoneyChance; } }

    public float mapWidth { get; private set; }
    public float mapHeight { get; private set; }
    public float mapDepth { get; private set; }
    public float obstacleScale { get; private set; }
    public float spawnY { get; private set; }

    public Map(float mapWidth, float mapHeight, float mapDepth, float obstacleScale)
    {
        obstacles = new List<GameObject>();
        this.mapHeight = mapHeight;
        this.mapWidth = mapWidth;
        this.mapDepth = mapDepth;
        this.obstacleScale = obstacleScale;
        spawnY = mapHeight - 1.5f * obstacleScale;
    }
}
