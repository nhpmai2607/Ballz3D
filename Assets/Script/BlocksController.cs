using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour {
    public GameObject blockPrefab;
    private List<GameObject> currentBlocks;
    public System.Random rnd;

    private void Awake()
    {
        currentBlocks = new List<GameObject>();
        rnd = new System.Random();
    }

    private void Start()
    {
        spawnBlock();
    }

    public void removeBlock(GameObject block)
    {
        BlockController blockController = block.GetComponent<BlockController>();
        if (blockController.DecreaseHealth())
        {
            currentBlocks.Remove(block);
            Destroy(block);
        }
    }

    public void spawnBlock()
    {
        List<Vector3> currentPos = new List<Vector3>();
        int numBlocks = rnd.Next(1, 4);
        for (int i = 0; i < numBlocks; i++)
        {
            Vector3 newPos = getNewPos(currentPos);
            currentPos.Add(newPos);

            GameObject newBlock = Instantiate(blockPrefab, newPos, Quaternion.identity, transform);
            currentBlocks.Add(newBlock);
        }
    }

    public void getBlockDown()
    {
        foreach (GameObject block in currentBlocks)
        {
            block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - 0.2f, block.transform.position.z);
        }
    }

    private Vector3 getNewPos(List<Vector3> currentPos)
    {
        Vector3 newPos;
        do
        {
            newPos = new Vector3(RandomPos(), 1.7f, RandomPos());
        } while (currentPos.Contains(newPos));

        return newPos;
    }

    private float RandomPos()
    {
        return (RandomOddNumber(0, 10) - 4f) / 10f;
    }

    private int RandomOddNumber(int minValue, int maxValue)
    {
        int num = rnd.Next(minValue, maxValue);
        return num % 2 == 0 ? num : num - 1;
    }
}
