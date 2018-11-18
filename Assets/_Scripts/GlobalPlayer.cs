using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class GlobalPlayer
{
    public static Dictionary<PartType, int> selectedParts;
    public static String playerName;
    public static bool playerNameIsDefault = true;
    public static int bank;
    public static List<int> unlockIds;
    public static bool isSetup;

    public static void SetupPlayer()
    {
        selectedParts = new Dictionary<PartType, int>();
        playerName = "Test";
        bank = 0;
        unlockIds = new List<int>();

        foreach (PartType type in Enum.GetValues(typeof(PartType)))
        {
            selectedParts[type] = 1;
        }

        isSetup = true;
    }

    public static void AddScore(int score)
    {
        GameObject.FindObjectOfType<ScoreKeeper>().AddScore(playerName, score);
    }

    public static Dictionary<PartType, int> AllParts()
    {
        if (!isSetup)
            SetupPlayer();

        return selectedParts;
    }

    public static void SetPart(PartType type, int index)
    {
        if (!isSetup)
            SetupPlayer();

        selectedParts[type] = index;
    }

    public static void SetPlayerName(String inputPlayerName)
    {
        if (inputPlayerName == "")
        {
            playerNameIsDefault = true;
            playerName = "Player1";
        }

        else
        {
            playerNameIsDefault = false;
            playerName = inputPlayerName;
        }
    }

    public static int IndexForType(PartType type)
    {
        if (!isSetup)
            SetupPlayer();
            
        return selectedParts[type];
    }

    public static void AddMoney(int money)
    {
        if (!isSetup)
            SetupPlayer();

        bank += money;
    }

    public static void UnlockItem(int unlockId, int cost)
    {
        if (!isSetup)
            SetupPlayer();

        unlockIds.Add(unlockId);
        bank -= cost;
    }

    public static bool HasEnoughMoney(int cost)
    {
        if (!isSetup)
            SetupPlayer();

        return bank >= cost;
    }
}
