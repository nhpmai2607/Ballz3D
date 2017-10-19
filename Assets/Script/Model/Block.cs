using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {
    private int _health;
    public int health {
        get { return _health; }
        set { _health = Mathf.Max(0, value); }
    }

    public Block()
    {
        health = 1;
    }

}
