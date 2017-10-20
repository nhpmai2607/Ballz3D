using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLoading : MonoBehaviour {
    public Toggle backgroundButton;
    AudioSource background;

    public Toggle effectButton;
    public AudioSource effect;

	// Use this for initialization
	void Start () {
        background = GetComponent<AudioSource>();
        background.volume = PlayerPrefs.HasKey("BackgroundSound") ? PlayerPrefs.GetFloat("BackgroundSound") : 1f;
        background.mute = PlayerPrefs.HasKey("BackgroundSoundMute") && PlayerPrefs.GetInt("BackgroundSoundMute") == 1 ? true : false;
        backgroundButton.isOn = !background.mute;

        setEffectSound();
    }

    public void setEffectSound()
    {
        if (effect != null && effectButton != null)
        {
            effect.volume = PlayerPrefs.HasKey("EffectSound") ? PlayerPrefs.GetFloat("EffectSound") : 1f;
            effect.mute = PlayerPrefs.HasKey("EffectSoundMute") && PlayerPrefs.GetInt("EffectSoundMute") == 1 ? true : false;
            effectButton.isOn = !effect.mute;
        }
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
        if (effect != null)
        {
            effect.mute = !isOn;
        }
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
