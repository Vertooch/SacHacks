using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreStar : MonoBehaviour {

    private Image starImage;

    public void Activate()
    {
        starImage = GetComponent<Image>();
        starImage.color = new Color(255, 255, 255, 255);
    }
}
