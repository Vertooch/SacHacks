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
            GlobalPlayer.UnlockItem(item.id, item.cost);
            Debug.Log("buy item: " + item.id);
            Debug.Log("current money: " + GlobalPlayer.bank);
        }
        else
            Debug.Log("not enough money");
    }

}
