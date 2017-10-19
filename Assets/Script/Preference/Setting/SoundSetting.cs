using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour {

    public Slider background;
    public Slider effect;
    public AudioSource music;

	// Use this for initialization
	void Start () {
        background.value = PlayerPrefs.HasKey("BackgroundSound") ? PlayerPrefs.GetFloat("BackgroundSound") : 1f;
        effect.value = PlayerPrefs.HasKey("EffectSound") ? PlayerPrefs.GetFloat("EffectSound") : 1f;
    }
	
    public void onBackgroundSoundChanged(float value) {
        PlayerPrefs.SetFloat("BackgroundSound", value);
        music.volume = value;
        PlayerPrefs.Save();
    }

    public void onEffectSoundChanged(float value)
    {
        PlayerPrefs.SetFloat("EffectSound", value);
        PlayerPrefs.Save();
    }
}
