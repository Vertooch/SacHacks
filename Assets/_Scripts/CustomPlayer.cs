using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayer : MonoBehaviour
{
    public Transform head;
    public Transform eyes;
    public Transform hat;
    public Transform mouth;
    public Transform mustache;
    public Transform body;
    public Transform weapon;
    public AvatarInventory inventory;
    public Text avatarPartText;

    private Transform[] avatarParts;

    private int partTypeIndex;
    private int[] partIndices;
    private List<PartType> types;

    public void Start()
    {
        avatarParts = new Transform[] { head, eyes, hat, mouth, mustache, body, weapon };
        types = new List<PartType>();
        partIndices = new int[] {0, 0, 0, 0, 0, 0, 0};

        foreach (PartType type in Enum.GetValues(typeof(PartType)))
        {
            types.Add(type);
        }
    }

    public void IncrementPartOption()
    {
        // Destroy children of current part
        foreach (Transform part in avatarParts[partTypeIndex])
        {
            Destroy(part.gameObject);
        }

        if (partIndices[partTypeIndex] < (inventory.inventoryParts[types[partTypeIndex]].Length - 1))
        {
            partIndices[partTypeIndex]++;
        }

        else
        {
            partIndices[partTypeIndex] = 0;
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            inventory.partForTypeIndex(types[partTypeIndex], partIndices[partTypeIndex]),
            avatarParts[partTypeIndex],
            false);
    }

    public void DecrementPartOption()
    {
        // Destroy children of current part
        foreach (Transform part in avatarParts[partTypeIndex])
        {
            Destroy(part.gameObject);
        }

        // Descrement the index for the part
        if (partIndices[partTypeIndex] > 0)
        {
            partIndices[partTypeIndex]--;
        }

        else
        {
            partIndices[partTypeIndex] = inventory.inventoryParts[types[partTypeIndex]].Length - 1;
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            inventory.partForTypeIndex(types[partTypeIndex], partIndices[partTypeIndex]),
            avatarParts[partTypeIndex],
            false);
    }

    public void IncrementAvatarPart()
    {
        Debug.Log("Increment Part Type");

        if (partTypeIndex < (types.Count - 1))
        {
            partTypeIndex++;
        }

        else
        {
            partTypeIndex = 0;
        }

        avatarPartText.text = Enum.GetName(typeof(PartType), types[partTypeIndex]);
    }

    public void DecrementAvatarPart()
    {
        Debug.Log("Decrement Part Type");

        if (partTypeIndex > 0)
        {
            partTypeIndex--;
        }

        else
        {
            partTypeIndex = types.Count - 1;
        }

        avatarPartText.text = Enum.GetName(typeof(PartType), types[partTypeIndex]);
    }
}
