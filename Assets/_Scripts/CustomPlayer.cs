using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayer : MonoBehaviour {

    public SpriteRenderer head;
    public Sprite[] heads;

    private int headIndex = 0;
    

    public void SwapHeadRight()
    {
        Debug.Log("swap head right");
        headIndex++;
        head.sprite = heads[headIndex];
    }

    public void SwapHeadLeft()
    {
        Debug.Log("swap head left");
        headIndex--;
        head.sprite = heads[headIndex];
    }

}
