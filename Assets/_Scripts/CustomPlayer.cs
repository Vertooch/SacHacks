using System.Collections;
using System.Collections.Generic;
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

    public GameObject[] headOptions;
    public GameObject[] eyesOptions;
    public GameObject[] hatOptions;
    public GameObject[] mouthOptions;
    public GameObject[] mustacheOptions;
    public GameObject[] bodyOptions;
    public GameObject[] weaponOptions;

    public Text avatarPartText;

    private Transform[] avatarParts;
    private GameObject[][] partOptions;

    private int avatarPartIndex;
    private int[] partOptionsIndices;
    private string[] avatarPartName;

    public void Start()
    {
        avatarParts = new Transform[] { head, eyes, hat, mouth, mustache, body, weapon };
        partOptions = new GameObject[][] { headOptions, eyesOptions, hatOptions, mouthOptions, mustacheOptions, bodyOptions, weaponOptions };
        avatarPartName = new string[] { "Head", "Eyes", "Hat", "Mouth", "Mustache", "Body", "Weapon" };

        avatarPartIndex = 0;
        partOptionsIndices = new int[avatarParts.Length];
    }

    public void IncrementPartOption()
    {
        // Destroy children of current part
        foreach (Transform part in avatarParts[avatarPartIndex])
        {
            Destroy(part.gameObject);
        }

        // Increment the index for the part
        if (partOptionsIndices[avatarPartIndex] < (partOptions[avatarPartIndex].Length - 1))
        {
            partOptionsIndices[avatarPartIndex]++;
        }

        else
        {
            partOptionsIndices[avatarPartIndex] = 0;
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            partOptions[avatarPartIndex][partOptionsIndices[avatarPartIndex]],
            avatarParts[avatarPartIndex],
            false);
    }

    public void DecrementPartOption()
    {
        // Destroy children of current part
        foreach (Transform part in avatarParts[avatarPartIndex])
        {
            Destroy(part);
        }

        // Descrement the index for the part
        if (partOptionsIndices[avatarPartIndex] > 0)
        {
            partOptionsIndices[avatarPartIndex]--;
        }

        else
        {
            partOptionsIndices[avatarPartIndex] = partOptions[avatarPartIndex].Length - 1;
        }

        // Instantiate new children in parent GameObject
        Instantiate(
            partOptions[avatarPartIndex][partOptionsIndices[avatarPartIndex]],
            avatarParts[avatarPartIndex],
            false);
    }

    public void IncrementAvatarPart()
    {
        Debug.Log("Increment Part Type");

        if (avatarPartIndex < (avatarParts.Length - 1))
        {
            avatarPartIndex++;
        }

        else
        {
            avatarPartIndex = 0;
        }

        avatarPartText.text = avatarPartName[avatarPartIndex];
    }

    public void DecrementAvatarPart()
    {
        Debug.Log("Decrement Part Type");

        if (avatarPartIndex > 0)
        {
            avatarPartIndex--;
        }

        else
        {
            avatarPartIndex = avatarParts.Length - 1;
        }

        avatarPartText.text = avatarPartName[avatarPartIndex];
    }
}
