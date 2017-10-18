using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour {
    int health;
    System.Random rnd;

    public Material[] materials;

    public Color defaultColor;
    PlayerController playerController;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = PlayerPrefs.HasKey("Block") && materials.Length > PlayerPrefs.GetInt("Block")
            ? materials[PlayerPrefs.GetInt("Block")] : materials[0];
    }

    // Use this for initialization
    void Start () {
        rnd = GameObject.Find("BlocksController").GetComponent<BlocksController>().rnd;
        playerController = GameObject.Find("CustomCamera").GetComponent<PlayerController>();
        int count = (int) Mathf.Max(1, playerController.player.count * 1.8f);
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
                playerController.serializePlayer();
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
}
