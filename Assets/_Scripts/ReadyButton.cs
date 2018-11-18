using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour {

    public Text readyText;
    private ExamplePlayerScript player;
    private bool isReady;


    public void Setup(ExamplePlayerScript _player)
    {
        player = _player;
    }

    public void ChangeState()
    {
        isReady = !isReady;
        player.SetReady(isReady);
    }

}
