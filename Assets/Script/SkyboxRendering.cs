using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRendering : MonoBehaviour {
    public Material[] backgrounds;

	// Use this for initialization
	void Start () {
        RenderSettings.skybox = PlayerPrefs.HasKey("Background") && backgrounds.Length > PlayerPrefs.GetInt("Background")
            ? backgrounds[PlayerPrefs.GetInt("Background")] : backgrounds[0];
    }
}
