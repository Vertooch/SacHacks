using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void GoToCustomize()
    {
        SceneManager.LoadScene("Custom2");
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard2");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Test2");
    }
}
