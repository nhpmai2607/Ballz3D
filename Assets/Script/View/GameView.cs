﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour {
    public Text numBallsText;
    public Text countText;
    public GameObject ballPrefab;

    public void UpdateNumBallsText(int numBalls)
    {
        numBallsText.text = "x" + numBalls;
    }

    public void UpdateCountText(int count)
    {
        countText.text = "" + count;
    }

    public void UpdateView(int numBalls, int count)
    {
        UpdateNumBallsText(numBalls);
        UpdateCountText(count);
    }

    public GameObject InstantiateBouncingBall(Vector3 mainBallPos, Quaternion mainBallRot)
    {
        return Instantiate(ballPrefab, mainBallPos, mainBallRot, transform);
    }
}
