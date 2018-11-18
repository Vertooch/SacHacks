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

    public Transform prevPartButton;
    public Transform nextPartButton;

    public Button prevPartTypeButton;
    public Button nextPartTypeButton;

    public AvatarInventory inventory;
    public Text avatarPartText;

    private Transform[] avatarParts;

    private int partTypeIndex;
    private int[] partIndices;
    private List<PartType> types;

    private void UpdatePrevNextPartTypeText()
    {
        int prevPartTypeIndex;
        int nextPartTypeIndex;

        // Set previous and next text indices 
        prevPartTypeIndex = partTypeIndex == 0 ? types.Count - 1 : partTypeIndex - 1;
        nextPartTypeIndex = partTypeIndex == (types.Count - 1) ? 0 : partTypeIndex + 1;

        // Update the button text
        prevPartTypeButton.GetComponentInChildren<Text>().text = 
            Enum.GetName(typeof(PartType), types[prevPartTypeIndex]);

        nextPartTypeButton.GetComponentInChildren<Text>().text =
            Enum.GetName(typeof(PartType), types[nextPartTypeIndex]);
    }

    private void UpdatePrevNextButtons()
    {
        int prevPartIndex;
        int nextPartIndex;
        int maxPartIndex;

        maxPartIndex = inventory.inventoryParts[types[partTypeIndex]].Length - 1;

        // Set previous part index
        prevPartIndex = partIndices[partTypeIndex] == 0 ? maxPartIndex : partIndices[partTypeIndex] - 1;
        nextPartIndex = partIndices[partTypeIndex] == maxPartIndex ? 0 : partIndices[partTypeIndex] + 1;

        // Destroy and instantiate next and previous buttons
        foreach (Transform prevButton in prevPartButton)
        {
            Destroy(prevButton.gameObject);
        }

        foreach (Transform nextButton in nextPartButton)
        {
            Destroy(nextButton.gameObject);
        }

        GameObject tNextButton = 
            Instantiate(inventory.partForTypeIndex(types[partTypeIndex], nextPartIndex),
                        nextPartButton,
                        false);

        GameObject tPrevButton =
            Instantiate(inventory.partForTypeIndex(types[partTypeIndex], prevPartIndex),
                        prevPartButton,
                        false);

        // Scale the resulting buttons
        tNextButton.transform.localScale = new Vector3(50, 50, 1);
        tPrevButton.transform.localScale = new Vector3(50, 50, 1);

    }

    public void Start()
    {
        avatarParts = new Transform[] { head, eyes, hat, mouth, mustache, body, weapon };
        types = new List<PartType>();
        partIndices = new int[] {0, 0, 0, 0, 0, 0, 0};

        foreach (PartType type in Enum.GetValues(typeof(PartType)))
        {
            types.Add(type);
        }

        // Instantiate initial next and prev button objects
        UpdatePrevNextButtons();

        // Initialize button text
        UpdatePrevNextPartTypeText();

    }

    public void IncrementPartOption()
    {
        int maxPartIndex;

        maxPartIndex = inventory.inventoryParts[types[partTypeIndex]].Length - 1;

        // Destroy children of current part
        foreach (Transform part in avatarParts[partTypeIndex])
        {
            Destroy(part.gameObject);
        }

        if (partIndices[partTypeIndex] < maxPartIndex)
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

        UpdatePrevNextButtons();
    }

    public void DecrementPartOption()
    {
        int maxPartIndex;

        maxPartIndex = inventory.inventoryParts[types[partTypeIndex]].Length - 1;

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
            partIndices[partTypeIndex] = maxPartIndex;
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            inventory.partForTypeIndex(types[partTypeIndex], partIndices[partTypeIndex]),
            avatarParts[partTypeIndex],
            false);

        UpdatePrevNextButtons();
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

        UpdatePrevNextButtons();
        UpdatePrevNextPartTypeText();
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

        UpdatePrevNextButtons();
        UpdatePrevNextPartTypeText();
    }
}
