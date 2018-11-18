using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour {

    protected ExamplePlayerScript localPlayer;

	// Use this for initialization
	void Start () {
		foreach(ExamplePlayerScript player in GameObject.FindObjectsOfType<ExamplePlayerScript>())
        {
            if (player.isLocalPlayer)
                localPlayer = player;
        }
	}
	
}
