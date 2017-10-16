using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class CountController : MonoBehaviour {
    public Text countText;
    public Text bestScoreText;

    public Score score { get; set; }

    private void Awake()
    {
        score = new Score();
        deserializeScore();
        bestScoreText.text = "Best: " + score.highestScore;
        countText.text = "" + score.count;
    }

    public void increaseCount()
    {
        score.count += 1;
        countText.text = "" + score.count;
    }

    public void serializeScore()
    {
        score.highestScore = Mathf.Max(score.highestScore, score.count);
        try
        {
            using (Stream stream = File.Open("score.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, score);
            }
        }
        catch (IOException)
        {
        }
    }

    public void deserializeScore()
    {
        try
        {
            using (Stream stream = File.Open("score.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                score = (Score) bin.Deserialize(stream);
            }
        }
        catch (IOException)
        {
        }
    }
}
