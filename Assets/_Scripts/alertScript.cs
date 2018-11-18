using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class alertScript : MonoBehaviour {
    public static Text _alertText;
    // Use this for initialization
    void Start()
    {
        _alertText = GetComponent<Text>();
        _alertText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
