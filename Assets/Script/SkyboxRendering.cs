using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRendering : MonoBehaviour {
    public Material[] backgrounds;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Background") && backgrounds.Length > PlayerPrefs.GetInt("Background"))
        {
            RenderSettings.skybox = backgrounds[PlayerPrefs.GetInt("Background")];
        }
        else
        {
            RenderSettings.skybox = backgrounds[0];
        }
    }
}
