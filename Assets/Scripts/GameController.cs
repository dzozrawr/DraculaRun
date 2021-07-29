using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    public TimePassed timePassed;
    public DebugScreen debugScreen;


    public void GameOver()
    {
        Time.timeScale = 0f;    //pauses the game
        float time = timePassed.GetComponent<TimePassed>().getTime();
        gameOverScreen.Setup(time); //opens game over screen
    }

    public void DebugMenu()
    {
        Time.timeScale = 0;
        debugScreen.Setup();
    }
}
