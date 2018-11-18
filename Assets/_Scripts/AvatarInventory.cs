using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInventory : MonoBehaviour {

    public BodyPart[] headOptions;
    public BodyPart[] eyesOptions;
    public BodyPart[] hatOptions;
    public BodyPart[] mouthOptions;
    public BodyPart[] mustacheOptions;
    public BodyPart[] bodyOptions;
    public BodyPart[] weaponOptions;

    public Dictionary<PartType, BodyPart[]> inventoryParts;

    // Use this for initialization
    void Start () {
        if (inventoryParts == null)
            SetupInventory();
    }

    public void SetupInventory()
    {
        inventoryParts = new Dictionary<PartType, BodyPart[]>();

        inventoryParts[PartType.Head] = headOptions;
        inventoryParts[PartType.Eyes] = eyesOptions;
        inventoryParts[PartType.Hat] = hatOptions;
        inventoryParts[PartType.Mouth] = mouthOptions;
        inventoryParts[PartType.Mustache] = mustacheOptions;
        inventoryParts[PartType.Body] = bodyOptions;
        inventoryParts[PartType.Weapon] = weaponOptions;
    }

    public GameObject partForTypeIndex(PartType type, int index)
    {
        if (inventoryParts == null)
            SetupInventory();

        return inventoryParts[type][index].gameObject;
    }

    public void SetInventoryParts(PartType type, BodyPart[] bodyParts)
    {
        switch (type)
        {
            case PartType.Body:
                bodyOptions = bodyParts;
                break;
            case PartType.Head:
                headOptions = bodyParts;
                break;
            case PartType.Eyes:
                eyesOptions = bodyParts;
                break;
            case PartType.Hat:
                hatOptions = bodyParts;
                break;
            case PartType.Weapon:
                weaponOptions = bodyParts;
                break;
            case PartType.Mouth:
                mouthOptions = bodyParts;
                break;
            case PartType.Mustache:
                mustacheOptions = bodyParts;
                break;
        }
    }

}
