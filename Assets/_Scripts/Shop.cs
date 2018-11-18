using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public void AddMoney()
    {
        GlobalPlayer.AddMoney(100);
        Debug.Log("current money: " + GlobalPlayer.bank);
    }

    public void BuyItem(ShopItem item)
    {
        if (GlobalPlayer.HasEnoughMoney(item.cost))
        {
            if (GlobalPlayer.unlockIds.Contains(item.id))
            {
                Debug.Log("Already purchased item");
                alertScript._alertText.text = "Already purchased item";
                alertScript._alertText.color = Color.red;
            }
            else
            {
                GlobalPlayer.UnlockItem(item.id, item.cost);
                Debug.Log("buy item: " + item.id);
                Debug.Log("current money: " + GlobalPlayer.bank);
                alertScript._alertText.text = "Item purchased!";
                alertScript._alertText.color = Color.yellow;
            }
        }
        else
        {
            Debug.Log("not enough money");
            alertScript._alertText.text = "Not enough money";
            alertScript._alertText.color = Color.red;
        }
    }

}
