using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour {

    public Text messageText;
    public Text titleText;

    public void AddMoney()
    {
        GlobalPlayer.AddMoney(100);
        Debug.Log("current money: " + GlobalPlayer.bank);
    }

    public void BuyItem(ShopItem item)
    {
        if (messageText == null)
            messageText = gameObject.GetComponentInChildren<Text>();

        if (GlobalPlayer.HasEnoughMoney(item.cost))
        {
            if (GlobalPlayer.unlockIds.Contains(item.id))
            {
                messageText.text = "Already purchased!";
            }

            else
            {
                GlobalPlayer.UnlockItem(item.id, item.cost);
                messageText.text = "Item Purchased!";
            }
        }
        else
            messageText.text = "Not Enough Money!";
    }
}
