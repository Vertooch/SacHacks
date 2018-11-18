using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExampleListener : CaptainsMessListener
{
	public enum NetworkState
	{
		Init,
		Offline,
		Connecting,
		Connected,
		Disrupted
	};
	[HideInInspector]
	public NetworkState networkState = NetworkState.Init;

    public GameObject[] games;
    public GameObject gameSessionPrefab;
	public ExampleGameSession gameSession;

    public void Start()
	{
		networkState = NetworkState.Offline;

		ClientScene.RegisterPrefab(gameSessionPrefab);

        foreach (GameObject game in games)
        {
            Debug.Log(ClientScene.prefabs.Count);
            Debug.Log("REGISTER " + game.name);
            ClientScene.RegisterPrefab(game);
            Debug.Log(ClientScene.prefabs.Count);
        }

        foreach (GameObject go in ClientScene.prefabs.Values)
        {
            Debug.Log(go.name);
        }

        GameObject.Find("Canvas").SetActive(true);
    }

	public override void OnStartConnecting()
	{
		networkState = NetworkState.Connecting;
	}

	public override void OnStopConnecting()
	{
		networkState = NetworkState.Offline;
	}

	public override void OnServerCreated()
	{
		// Create game session
		ExampleGameSession oldSession = FindObjectOfType<ExampleGameSession>();
		if (oldSession == null)
		{
            GameObject serverSession = Instantiate(gameSessionPrefab);
            serverSession.GetComponent<ExampleGameSession>().SetGames(games);

            NetworkServer.Spawn(serverSession);
		}
		else
		{
			Debug.LogError("GameSession already exists!");
		}
	}

	public override void OnJoinedLobby()
	{
		networkState = NetworkState.Connected;

		gameSession = FindObjectOfType<ExampleGameSession>();
		if (gameSession) {
			gameSession.OnJoinedLobby();
		}
	}

	public override void OnLeftLobby()
	{
		networkState = NetworkState.Offline;

		gameSession.OnLeftLobby();
	}

	public override void OnCountdownStarted()
	{
		gameSession.OnCountdownStarted();
	}

	public override void OnCountdownCancelled()
	{
		gameSession.OnCountdownCancelled();
	}

	public override void OnStartGame(List<CaptainsMessPlayer> aStartingPlayers)
	{
		Debug.Log("GO!");
		gameSession.OnStartGame(aStartingPlayers);
	}

	public override void OnAbortGame()
	{
		Debug.Log("ABORT!");
		gameSession.OnAbortGame();
	}

    public void GameOver()
    {
        Destroy(gameSession.gameObject);
    }
}
