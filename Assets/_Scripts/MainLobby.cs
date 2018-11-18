using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobby : MonoBehaviour {

    public CaptainsMess mess;
    public GameObject homeButton;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("main lobby start");
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
