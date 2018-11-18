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
        return inventoryParts[type][index].gameObject;
    }
}
