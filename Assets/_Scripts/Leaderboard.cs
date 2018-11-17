using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    public GameObject scoreEntry;
    public GameObject scrollContent;
    public int numScores;

    // Use this for initialization
    void Start () {

        for (int i = 0; i < numScores; i++)
        {
            GameObject score = Instantiate(scoreEntry, scrollContent.transform, false) as GameObject;
            score.GetComponent<Text>().text = "Score " + (i + 1);
        }
	}
	
}
