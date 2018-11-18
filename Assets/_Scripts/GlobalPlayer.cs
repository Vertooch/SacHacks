using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayer : MonoBehaviour
{

    private Dictionary<PartType, int> selectedParts;

    // Use this for initialization
    void Start()
    {
        selectedParts = new Dictionary<PartType, int>();
    }

    // Update is called once per frame
    public void SetPart(PartType type, int index)
    {
        selectedParts[type] = index;
    }
}
