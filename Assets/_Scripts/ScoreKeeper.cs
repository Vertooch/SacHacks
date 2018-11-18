using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    const string privateCode = "GckICj3BkECs3qAnbYQuYwN8g2UArvLUWFeXUdoTCWZQ";
    const string publicCode = "5bf06a0a4cc83c0d54a6d915";
    const string webUrl = "http://dreamlo.com/lb/";

    public void AddScore(string name, int score)
    {
        StartCoroutine(UploadNewHighScore(name, score));
    }

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new WWW(webUrl + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("success!");
        }
        else
        {
            print("failed! " + www.error);
        }
    }

}
