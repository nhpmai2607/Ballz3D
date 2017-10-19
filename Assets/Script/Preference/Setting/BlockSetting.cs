using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSetting : MonoBehaviour {
    public Button backButton;
    public Button nextButton;
    public Material[] materials;
    public GameObject customCamera;
    MeshRenderer objRenderer;
    List<int> playerMaterials;

    CameraController cameraController;
    int playerMatIndex;
    float autoRotSpeed;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(backgrounds.Length);
        autoRotSpeed = 50f;
        objRenderer = GetComponent<MeshRenderer>();
        cameraController = customCamera.GetComponent<CameraController>();
        playerMaterials = customCamera.GetComponent<PlayerController>().player.materials;
        playerMatIndex = PlayerPrefs.HasKey("Block") && playerMaterials.Contains(PlayerPrefs.GetInt("Block"))
            ? playerMaterials.IndexOf(PlayerPrefs.GetInt("Block")) : 0;
        setAction();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, autoRotSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(cameraController.target, -Vector3.up, cameraController.rotSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(cameraController.target, Vector3.up, cameraController.rotSpeed * Time.deltaTime);
        }
    }

    public void onClickBackButton()
    {
        playerMatIndex = Mathf.Max(0, --playerMatIndex);
        setAction();
        //Debug.Log("Click Back Button " + index);
    }

    public void onClickNextButton()
    {
        playerMatIndex = Mathf.Min(++playerMatIndex, playerMaterials.Count - 1);
        setAction();
        //Debug.Log("Click Next Button " + index);
    }

    private void setAction()
    {
        objRenderer.material = materials[playerMaterials[playerMatIndex]];
        checkButtonsState();
        PlayerPrefs.SetInt("Block", playerMaterials[playerMatIndex]);
        PlayerPrefs.Save();
    }

    public void checkButtonsState()
    {
        backButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        if (playerMatIndex == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        if (playerMatIndex == playerMaterials.Count - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
    }
}
