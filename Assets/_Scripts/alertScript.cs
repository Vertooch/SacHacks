using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class alertScript : MonoBehaviour {
    public Text _alertText;
    // Use this for initialization
    void Start()
    {
        _alertText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _alertText.text = ";
    }
}
