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

    public InputField playerNameField;
    
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

        UpdateNullBodyPart(tPrevButton, types[partTypeIndex], prevPartIndex, true);
        UpdateNullBodyPart(tNextButton, types[partTypeIndex], nextPartIndex, true);

    }

    private void UpdateNullBodyPart(GameObject bodyPart, PartType type, int index, bool enable)
    {
        // Remove "null" body parts (no hat, no weapon, etc...)
        switch (type)
        {
            default:
                break;
            case PartType.Mustache:
            case PartType.Hat:
            case PartType.Weapon:
                if (index == 0)
                {
                    // Turn off the SpriteRenderer
                    bodyPart.GetComponent<SpriteRenderer>().enabled = enable;

                    // Scale down the object
                    bodyPart.transform.localScale = new Vector3(25, 25, 1);
                }
                break;
        }
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

        // Initialize button text
        UpdatePrevNextPartTypeText();

        // Prevent any null body parts from being rendered
        for (int i = 0; i < types.Count; i++)
        {
            foreach (Transform part in avatarParts[i])
            {
                UpdateNullBodyPart(part.gameObject, types[i], partIndices[i], false);
            }
        }

        // Instantiate initial next and prev button objects
        UpdatePrevNextButtons();

        // Add listener for input field
        playerNameField.onValueChanged.AddListener(delegate { GlobalPlayer.SetPlayerName(playerNameField.text); } );

        // Set text if player has set a name before
        if (!GlobalPlayer.playerNameIsDefault)
        {
            playerNameField.text = GlobalPlayer.playerName;
        }
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
        GameObject bodyPart = Instantiate(
            inventory.partForTypeIndex(types[partTypeIndex], partIndices[partTypeIndex]),
            avatarParts[partTypeIndex],
            false);

        // Disable if current option is null
        UpdateNullBodyPart(bodyPart, types[partTypeIndex], partIndices[partTypeIndex], false);
            
        UpdatePrevNextButtons();
        GlobalPlayer.SetPart(types[partTypeIndex], partIndices[partTypeIndex]);
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
        GameObject bodyPart = Instantiate(
            inventory.partForTypeIndex(types[partTypeIndex], partIndices[partTypeIndex]),
            avatarParts[partTypeIndex],
            false);


        // Disable if current option is null
        UpdateNullBodyPart(bodyPart, types[partTypeIndex], partIndices[partTypeIndex], false);

        UpdatePrevNextButtons();
        GlobalPlayer.SetPart(types[partTypeIndex], partIndices[partTypeIndex]);
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
