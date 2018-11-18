using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Minigame : NetworkBehaviour
{
    [SyncVar]
    public NetworkInstanceId parentNetId;

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
