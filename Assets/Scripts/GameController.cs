using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    public TimePassed timePassed;
    public DebugScreen debugScreen;

    private void Awake()
    {
        Time.timeScale = 1f;    //if the game was paused
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        float time = timePassed.GetComponent<TimePassed>().getTime();
        gameOverScreen.Setup(time);
    }

    public void DebugMenu()
    {
        Time.timeScale = 0;
        debugScreen.Setup();
    }
}
