using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public class ExamplePlayerScript : CaptainsMessPlayer
{
	public Text readyField;

    [SyncVar]
    public string avatarOptions;
    [SyncVar]
    public string playerName;

    // Simple game states for a dice-rolling game

    [SyncVar]
	public int rollResult;

	[SyncVar]
	public int totalPoints;

    [SyncVar]
    public bool quit;

    [SyncVar]
    public int cash;

    private ScoreCard scoreCard;


	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();


        foreach(int val in GlobalPlayer.AllParts().Values)
        {
            avatarOptions += val.ToString();
            avatarOptions += ",";
        }
        avatarOptions = avatarOptions.Remove(avatarOptions.Length - 1);
        CmdSetAvatar(avatarOptions);

        playerName = GlobalPlayer.playerName;
        CmdSetName(playerName);
    }

    [Command]
    public void CmdSetName(string name)
    {
        playerName = name;
    }

    [Command]
    public void CmdSetAvatar(string avatarString)
    {
        avatarOptions = avatarString;
    }

    [Command]
	public void CmdRollDie()
	{
		rollResult = UnityEngine.Random.Range(1, 7);
	}

	[Command]
	public void CmdPlayAgain()
	{
		ExampleGameSession.instance.PlayAgain();
	}

	public override void OnClientEnterLobby()
	{
		base.OnClientEnterLobby();

		// Brief delay to let SyncVars propagate
		Invoke("ShowPlayer", 0.5f);
	}

	public override void OnClientReady(bool readyState)
	{
        if (readyField == null)
            return;

		if (readyState)
		{
			readyField.text = "READY!";
			readyField.color = Color.green;
		}
		else
		{
			readyField.text = "Not Ready";
			readyField.color = Color.red;
		}
	}

	void ShowPlayer()
	{
        transform.SetParent(GameObject.Find("Canvas/PlayerContainer/player"+(playerIndex+1)+"/avatar").transform, false);
        scoreCard = transform.parent.parent.gameObject.GetComponent<ScoreCard>();
        scoreCard.Setup(playerName);

        GetComponent<LobbyAvatar>().SetupAvatar(avatarOptions);

        readyField = transform.parent.gameObject.GetComponentInChildren<Text>();

		OnClientReady(IsReady());
	}

    public void Update()
    {
        if (quit)
            GameOver();

        if (totalPoints < 1)
            return;

        scoreCard.SetStars(totalPoints);
    }

    [ClientRpc]
	public void RpcOnStartedGame()
	{
		readyField.gameObject.SetActive(false);
        scoreCard.card.SetActive(true);
    }

    public void SetReady(bool isReady)
    {
        if (isReady)
        {
            SendNotReadyToBeginMessage();
        }
        else
        {
            SendReadyToBeginMessage();
        }

    }

    void OnGUI()
	{
		if (isLocalPlayer)
		{
			GUILayout.BeginArea(new Rect(0, Screen.height * 0.8f, Screen.width, 100));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			ExampleGameSession gameSession = ExampleGameSession.instance;
			if (gameSession)
			{
				if (gameSession.gameState == GameState.Lobby ||
					gameSession.gameState == GameState.Countdown)
				{
					if (GUILayout.Button(IsReady() ? "Not ready" : "Ready", GUILayout.Width(Screen.width * 0.3f), GUILayout.Height(100)))
					{
						if (IsReady()) {
							SendNotReadyToBeginMessage();
						} else {
							SendReadyToBeginMessage();
						}
					}
				}
				else if (gameSession.gameState == GameState.WaitingForRolls)
				{
					if (rollResult == 0)
					{
						if (GUILayout.Button("Roll Die", GUILayout.Width(Screen.width * 0.3f), GUILayout.Height(100)))
						{
							CmdRollDie();
						}
					}
				}
			}

			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
    	}
	}

    public void GameOver()
    {
        GlobalPlayer.bank += cash;
        GlobalPlayer.AddScore(totalPoints);
        GameObject.FindObjectOfType<MainLobby>().GameOver();
    }

}
