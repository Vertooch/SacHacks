﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAvatar : MonoBehaviour
{
    public Transform head;
    public Transform eyes;
    public Transform hat;
    public Transform mouth;
    public Transform mustache;
    public Transform body;
    public Transform weapon;
    public AvatarInventory inventory;
    public Text playerName;

    public void Start()
    {
        foreach (PartType type in Enum.GetValues(typeof(PartType)))
        {
            SetPartForType(type);
        }

        if (playerName != null)
            playerName.text = GlobalPlayer.playerName;
    }

    public void SetPartForType(PartType type)
    {
        Transform partTransform = null;

        switch (type)
        {
            case PartType.Body : 
                partTransform = body;
                break;
            case PartType.Eyes:
                partTransform = eyes;
                break;
            case PartType.Hat:
                partTransform = hat;
                break;
            case PartType.Head:
                partTransform = head;
                break;
            case PartType.Mouth:
                partTransform = mouth;
                break;
            case PartType.Mustache:
                partTransform = mustache;
                break;
            case PartType.Weapon:
                partTransform = weapon;
                break;
            default :
                partTransform = null;
                break;
        }

        if (partTransform == null || GlobalPlayer.IndexForType(type) == -1)
            return;

        // Destroy children of current part
        foreach (Transform part in partTransform)
        {
            Destroy(part.gameObject);
        }

        bool drawPart = true;

        // Instantiate new children in parent GameObject if the object is not empty (hat, weapon, etc)
        switch (type)
        {
            case PartType.Hat:
            case PartType.Weapon:
            case PartType.Mustache:
                if (GlobalPlayer.IndexForType(type) == 0)
                {
                    drawPart = false;
                }
                break;
        }

        if (drawPart)
        {
            Instantiate(
            inventory.partForTypeIndex(type, GlobalPlayer.IndexForType(type)),
            partTransform,
            false);
        }
    }

}