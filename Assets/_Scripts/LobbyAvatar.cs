using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyAvatar : MonoBehaviour
{
    public Transform head;
    public Transform eyes;
    public Transform hat;
    public Transform mouth;
    public Transform mustache;
    public Transform body;
    public Transform weapon;
    public AvatarInventory inventory;

    public void SetupAvatar(string avatarOptions)
    {
        inventory = GameObject.FindObjectOfType<AvatarInventory>();

        string[] splitOptions = avatarOptions.Split(',');

        for (int i = 0; i < splitOptions.Length; i++)
        {

            Debug.Log(splitOptions[i]);
            Debug.Log("Type: " + (PartType)i + " - Index: " + Int32.Parse(splitOptions[i]));
            SetPart((PartType)i, Int32.Parse(splitOptions[i]));

        }

        //for (int i = 0; i < partIndices.Count; i++)
        //{
        //    SetPart((PartType)i, partIndices[i]);
        //}
    }


    public void SetPart(PartType type, int partIndex)
    {
        Transform partTransform = null;

        switch (type)
        {
            case PartType.Body:
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
            default:
                partTransform = null;
                break;
        }

        if (partTransform == null)
            return;

        Debug.Log("set part: " + type + " - Index: " + partIndex);

        // Destroy children of current part
        foreach (Transform part in partTransform)
        {
            Destroy(part.gameObject);
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            inventory.partForTypeIndex(type, partIndex),
            partTransform,
            false);
    }

}