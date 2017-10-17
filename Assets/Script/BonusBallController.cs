using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallController : MonoBehaviour {
    BlocksController blocksController;

    private void Start()
    {
        blocksController = GameObject.Find("BlocksController").GetComponent<BlocksController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Ground":
                blocksController.removeBonusBall(this.gameObject);
                break;
        }
    }
}
