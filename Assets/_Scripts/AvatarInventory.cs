﻿using System.Collections;
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

    }
	

    public GameObject partForTypeIndex(PartType type, int index)
    {
        return inventoryParts[type][index].gameObject;
    }
}
