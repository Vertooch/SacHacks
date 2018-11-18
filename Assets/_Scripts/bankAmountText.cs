using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bankAmountText : MonoBehaviour {
    private Text _bankAmountText;
	// Use this for initialization
	void Start () {
        _bankAmountText = GetComponent<Text>();
	}
	
	// Update is called once per frame
    void Update () {
        _bankAmountText.text = "$" + GlobalPlayer.bank;
    }
}
