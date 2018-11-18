﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour {

    public GameObject card;
    public ScoreStar[] stars;
    public Text playerName;
    public Text readyField;

    private int numStars = 0;

    public void Setup(string name)
    {
        playerName.text = name;
        gameObject.SetActive(true);
        readyField.gameObject.SetActive(true);
    }

    public void SetStars(int newNumStars)
    {
        if (numStars < newNumStars)
            stars[newNumStars - 1].Activate();

        numStars = newNumStars - 1;
    }

}
