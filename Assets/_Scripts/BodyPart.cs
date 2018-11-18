using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Head,
    Eyes,
    Hat,
    Mouth,
    Mustache,
    Body,
    Weapon
}

public class BodyPart : MonoBehaviour {

    public PartType type;

}
