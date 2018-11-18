using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour {

    public GameObject card;
    public ScoreStar[] stars;
    public Text playerName;

    private int numStars = 0;

    public void Setup(string name)
    {
        playerName.text = name;
        gameObject.SetActive(true);
    }

    public void EarnStar()
    {
        stars[numStars].Activate();
        numStars++;
    }

}
