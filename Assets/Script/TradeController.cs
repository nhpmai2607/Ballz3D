using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour {

    public GameObject[] materialsPrice;
    public PlayerController playerController;
    public BallSetting ballSetting;
    public BlockSetting blockSetting;

    private void Start()
    {
        for (int i = 0; i < materialsPrice.Length; i++)
        {
            checkMaterialState(i);
        }
    }

    public void OnBuyMaterial(int index)
    {
        int price = (index + 1) * 5;
        if (price <= playerController.player.money)
        {
            materialsPrice[index].SetActive(false);
            playerController.player.money -= price;
            playerController.player.materials.Add(index + 1);
            playerController.serializePlayer();
            playerController.setMoneyText();
            ballSetting.checkButtonsState();
            blockSetting.checkButtonsState();
        }
    }

    private void checkMaterialState(int index)
    {
        if (playerController.player.materials.Contains(index + 1))
        {
            materialsPrice[index].SetActive(false);
        } else
        {
            materialsPrice[index].SetActive(true);
        }
    }
}
