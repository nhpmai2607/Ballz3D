using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour {
    public Block model { get; private set; }
    private BlockView view;

    GameController gameController;

    private void Awake()
    {
        model = new Block();
        view = GetComponent<BlockView>();
    }

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        int count = (int) Mathf.Max(1, gameController.model.count * 1.8f);
        model.health = Random.Range(1, count);
        view.SetColor(model.health);
	}

    public void DecreaseHealth()
    {
        model.health--;
        view.SetColor(model.health);
    }

    public bool IsAlive()
    {
        return model.health != 0 ? true : false;
    }

    private void OnCollisionEnterGround(Collision collision)
    {
        //Debug.Log("Collision Loseeee!!!");
        gameController.model.isGameOver = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                OnCollisionEnterGround(collision);
                break;
        }
    }
}
