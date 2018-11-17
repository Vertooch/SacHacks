using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    private void Awake()
    {
        DownloadHighScores();
    }

    const string privateCode = "GckICj3BkECs3qAnbYQuYwN8g2UArvLUWFeXUdoTCWZQ";
    const string publicCode = "5bf06a0a4cc83c0d54a6d915";
    const string webUrl = "http://dreamlo.com/lb/";
    private readonly int nu;
    public HighScore[] HighScoresList;

    public void AddNewHighScore(string username, int score) {
        StartCoroutine(UploadNewHighScore(username, score));
    }
    IEnumerator UploadNewHighScore(string username, int score) {
        WWW www = new WWW(webUrl + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            print("success!");
            //print(username);
            //print(score);
        } else {
            print("failed! " + www.error);
        }
    }
    IEnumerator DownloadHighScoresFromDatabase()
    {
        WWW www = new WWW(webUrl + publicCode + "/pipe/0/5");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
        }
        else
        {
            print("failed! " + www.error);
        }
    }
    public void DownloadHighScores() {
        StartCoroutine("DownloadHighScoresFromDatabase");
    }
    public GameObject scoreEntry;
    public GameObject scrollContent;
    public int numScores;
    public string[] scoreEntries;
    public struct HighScore {
        public string username;
        public int score;
        public HighScore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }

    void FormatHighScores(string textStream) {
        scoreEntries = new string[5];
        string[] entries = textStream.Split(new char[] {'\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        HighScoresList = new HighScore[entries.Length];
        numScores = entries.Length;
        if (numScores > 4)
        {
            numScores = 4;
        }
        for (int i = 0; i < numScores; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int scoreNum = int.Parse(entryInfo[1]);
            HighScoresList[i] = new HighScore(username, scoreNum);
            //print(HighScoresList[i].username + ": " + HighScoresList[i].score);
            scoreEntries[i] = HighScoresList[i].username + ": " + HighScoresList[i].score;
            GameObject score = Instantiate(scoreEntry, scrollContent.transform, false) as GameObject;
            score.GetComponent<Text>().text = scoreEntries[i];
        }
    }
    void Start () {
	}
	
}
