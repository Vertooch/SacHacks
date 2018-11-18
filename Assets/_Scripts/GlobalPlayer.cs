using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class GlobalPlayer
{
    public static Dictionary<PartType, int> selectedParts;
    public static String playerName;
    public static bool playerNameIsDefault = true;

    public static void SetupPlayer()
    {
        selectedParts = new Dictionary<PartType, int>();
        playerName = "Player1";

        foreach (PartType type in Enum.GetValues(typeof(PartType)))
        {
            selectedParts[type] = -1;
        }
    }

    public static void SetPart(PartType type, int index)
    {
        if (selectedParts == null)
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
        if (selectedParts == null)
            SetupPlayer();
            
        return selectedParts[type];
    }
}
