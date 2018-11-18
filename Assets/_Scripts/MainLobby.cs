using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobby : MonoBehaviour {

    public CaptainsMess mess;
    public GameObject homeButton;

    // Use this for initialization
    void Start () {
        Invoke("Connect", 1.0f);
	}

    void Connect(){
        mess.AutoConnect();
    }

    public void GameOver()
    {
        Debug.Log("lobby gameover");
        homeButton.SetActive(true);
    }
}
