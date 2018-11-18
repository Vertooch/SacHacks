using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickHits : Minigame {



    public void Hit()
    {
        System.Random rnd = new System.Random();
        localPlayer.CmdSubmitScore(rnd.Next(100));
    }




}
