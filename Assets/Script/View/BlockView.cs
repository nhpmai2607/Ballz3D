using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : MonoBehaviour {
    public Material[] materials;
    public Color defaultColor;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = PlayerPrefs.HasKey("Block") && materials.Length > PlayerPrefs.GetInt("Block")
            ? materials[PlayerPrefs.GetInt("Block")] : materials[0];
    }

    public void SetColor(int health)
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
}
