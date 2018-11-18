using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickHits : Minigame {



    public void Hit()
    {
        localPlayer.CmdSubmitScore(1);
    }




}
