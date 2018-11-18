using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum GameState
{
	Offline,
	Connecting,
	Lobby,
	Countdown,
	WaitingForScores,
	Scoring,
	GameOver
}

public class ExampleGameSession : NetworkBehaviour
{
	public Text gameStateField;
    public GameObject gameField;
    public GameObject[] games;
    private int gameIndex = 0;

	public static ExampleGameSession instance;

	ExampleListener networkListener;
	List<ExamplePlayerScript> players;
	string specialMessage = "";

	[SyncVar]
	public GameState gameState;

	[SyncVar]
	public string message = "";

	public void OnDestroy()
	{
		if (gameStateField != null) {
			gameStateField.text = "";
			gameStateField.gameObject.SetActive(false);
		}
	}

	[Server]
	public override void OnStartServer()
	{
		networkListener = FindObjectOfType<ExampleListener>();
		gameState = GameState.Connecting;
	}

	[Server]
	public void OnStartGame(List<CaptainsMessPlayer> aStartingPlayers)
	{
        foreach (GameObject game in games)
            ClientScene.RegisterPrefab(game);

        players = aStartingPlayers.Select(p => p as ExamplePlayerScript).ToList();

		RpcOnStartedGame();
		foreach (ExamplePlayerScript p in players) {
			p.RpcOnStartedGame();
		}

		StartCoroutine(RunGame());
	}

	[Server]
	public void OnAbortGame()
	{
		RpcOnAbortedGame();
	}

	[Client]
	public override void OnStartClient()
	{
		if (instance) {
			Debug.LogError("ERROR: Another GameSession!");
		}
		instance = this;

		networkListener = FindObjectOfType<ExampleListener>();
		networkListener.gameSession = this;

		if (gameState != GameState.Lobby) {
			gameState = GameState.Lobby;
		}
	}

	public void OnJoinedLobby()
	{
		gameState = GameState.Lobby;
	}

	public void OnLeftLobby()
	{
		gameState = GameState.Offline;
	}

	public void OnCountdownStarted()
	{
		gameState = GameState.Countdown;
	}

	public void OnCountdownCancelled()
	{
		gameState = GameState.Lobby;
	}

	[Server]
	IEnumerator RunGame()
	{
		// Reset game
		foreach (ExamplePlayerScript p in players) {
			p.totalPoints = 0;
		}

		while (MaxPoints() < 3)
		{
			// Reset rolls
			foreach (ExamplePlayerScript p in players) {
				p.score = 0;
			}

            SpawnGame();

			// Wait for all players to roll
			gameState = GameState.WaitingForScores;

			while (!AllPlayersHaveScored()) {
				yield return null;
			}

			// Award point to winner
			gameState = GameState.Scoring;

            ClearGame();

			List<ExamplePlayerScript> scoringPlayers = PlayersWithHighestScore();
			if (scoringPlayers.Count == 1)
			{
                scoringPlayers[0].totalPoints += 1;
                scoringPlayers[0].cash += 10;
                specialMessage = scoringPlayers[0].playerName + " scores 1 point!";
			}
			else
			{
				specialMessage = "TIE! No points awarded.";
			}

			yield return new WaitForSeconds(2);
			specialMessage = "";
		}

		// Declare winner!
        specialMessage = PlayerWithHighestScore().playerName + " WINS!";
		yield return new WaitForSeconds(3);

        foreach (ExamplePlayerScript p in players)
        {
            p.quit = true;
        }

        // Game over
        gameState = GameState.GameOver;
    }

    private void SpawnGame()
    {
        Debug.Log("spawning game..." + gameIndex);
        GameObject newGameObject = Instantiate(games[gameIndex], gameField.transform, false);
        Minigame newGame = newGameObject.GetComponent<Minigame>();

        gameIndex = (gameIndex + 1) % games.Length;
    }

    private void ClearGame()
    {
        foreach (Transform child in gameField.transform)
            Destroy(child.gameObject);
    }

    [Server]
    bool AllPlayersHaveScored()
	{
		return players.All(p => p.score != 0);
	}

	[Server]
    List<ExamplePlayerScript> PlayersWithHighestScore()
	{
        int highestScore = players.Max(p => p.score);
		return players.Where(p => p.score == highestScore).ToList();
	}

	[Server]
    int MaxPoints()
	{
		return players.Max(p => p.totalPoints);
	}

	[Server]
	ExamplePlayerScript PlayerWithHighestScore()
	{
		int highestScore = players.Max(p => p.totalPoints);
		return players.Where(p => p.totalPoints == highestScore).First();
	}

	[Server]
	public void PlayAgain()
	{
		//StartCoroutine(RunGame());
        gameState = GameState.Lobby;
    }

	void Update()
	{
		if (isServer)
		{
			if (gameState == GameState.Countdown)
			{
				message = "Math Starting in " + Mathf.Ceil(networkListener.mess.CountdownTimer()) + "...";
			}
			else if (specialMessage != "")
			{
				message = specialMessage;
			}
		}

		gameStateField.text = message;
	}

	// Client RPCs

	[ClientRpc]
	public void RpcOnStartedGame()
	{
	}

	[ClientRpc]
	public void RpcOnAbortedGame()
	{
	}
}
