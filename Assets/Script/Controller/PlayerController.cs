﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
    public Text countText;
    public Text bestScoreText;
    public Text moneyText;

    public Player player { get; private set; }

    private void Awake()
    {
        player = new Player();
        deserializePlayer();
        setBestScoreText();
        setCountText();
        setMoneyText();
    }

    private void setCountText()
    {
        if (countText != null)
        {
            countText.text = "" + PlayerPrefs.GetInt("Count", 0);
        }
    }

    private void setBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best: " + player.bestScore;
        }
    }

    public void setMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = "" + player.money;
        }
    }

    public void increaseMoney()
    {
        player.money += 1;
        setMoneyText();
        serializePlayer();
    }

    public void serializePlayer()
    {
        player.bestScore = Mathf.Max(player.bestScore, PlayerPrefs.GetInt("Count", 0));
        try
        {
            using (Stream stream = File.Open("player.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, player);
            }
        }
        catch (IOException)
        {
        }
    }

    public void deserializePlayer()
    {
        try
        {
            using (Stream stream = File.Open("player.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                player = (Player) bin.Deserialize(stream);
            }
        }
        catch (IOException)
        {
        }
    }
}
