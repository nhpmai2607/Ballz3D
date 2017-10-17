using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoading : MonoBehaviour {
    AudioSource background;

	// Use this for initialization
	void Start () {
        background = GetComponent<AudioSource>();
        background.volume = PlayerPrefs.HasKey("BackgroundSound") ? PlayerPrefs.GetFloat("BackgroundSound") : 1f;
        background.mute = PlayerPrefs.HasKey("BackgroundSoundMute") && PlayerPrefs.GetInt("BackgroundSoundMute") == 1 ? true : false;
    }

    public void onBackgroundSoundToggle(bool isOn)
    {
        background.mute = !isOn;

        if (isOn)
        {
            PlayerPrefs.SetInt("BackgroundSoundMute", 0);
        } else
        {
            PlayerPrefs.SetInt("BackgroundSoundMute", 1);
        }
        PlayerPrefs.Save();
    }

    public void onEffectSoundToggle(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("EffectSoundMute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("EffectSoundMute", 1);
        }
        PlayerPrefs.Save();
    }
}
