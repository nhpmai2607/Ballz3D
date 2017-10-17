using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour {
    int health;
    System.Random rnd;

    public Color defaultColor;
    CountController countController;

	// Use this for initialization
	void Start () {
        rnd = GameObject.Find("BlocksController").GetComponent<BlocksController>().rnd;
        countController = GameObject.Find("CountController").GetComponent<CountController>();
        int count = (int) Mathf.Max(1, countController.score.count * 1.8f);
        health = rnd.Next(1, count);
        setColor();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool DecreaseHealth()
    {
        health -= 1;
        if (health == 0)
        {
            return true;
        }
        setColor();
        return false;
    }

    void setColor()
    {
        Color tempColor = defaultColor;
        tempColor.b -= 0.2f * health;
        if (tempColor.b < 0f)
        {
            tempColor.g -= 0 - tempColor.b;
            tempColor.b = Mathf.Max(tempColor.b, 0f);
            tempColor.g = Mathf.Max(tempColor.g, 0f);
        }

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = tempColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                //Debug.Log("Collision Loseeee!!!");
                countController.serializeScore();
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
}
