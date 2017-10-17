using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSetting : MonoBehaviour {

    public Button backButton;
    public Button nextButton;
    public Material[] backgrounds;

    int index;

	// Use this for initialization
	void Start () {
        //Debug.Log(backgrounds.Length);
		if (PlayerPrefs.HasKey("Background") && backgrounds.Length > PlayerPrefs.GetInt("Background"))
        {
            index = PlayerPrefs.GetInt("Background");
        }
        else
        {
            index = 0;
        }
        checkButtonsState();
    }

    public void onClickBackButton()
    {
        index = Mathf.Max(0, --index);
        setAction();
        //Debug.Log("Click Back Button " + index);
    }

    public void onClickNextButton()
    {
        index = Mathf.Min(++index, backgrounds.Length - 1);
        setAction();
        //Debug.Log("Click Next Button " + index);
    }

    private void setAction()
    {
        RenderSettings.skybox = backgrounds[index];
        checkButtonsState();
        PlayerPrefs.SetInt("Background", index);
        PlayerPrefs.Save();
    }

    private void checkButtonsState()
    {
        backButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        if (index == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        if (index == backgrounds.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
    }
}
