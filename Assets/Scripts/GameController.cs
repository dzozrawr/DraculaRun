using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    public TimePassed timePassed;
    public DebugScreen debugScreen;

    private float highScore;

    private void Awake()
    {
        Time.timeScale = 1f;    //if the game was paused

        if (FileManager.LoadFromFile("HighScore.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            highScore = sd.highScore;
            Debug.Log("Load complete");
        }
        else
        {
            highScore = 0f;
        }
        Debug.Log("The high score is: " + highScore);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        float time = timePassed.GetComponent<TimePassed>().getTime();

        if (time > highScore)
        {
            highScore = time;
            SaveData sd = new SaveData();
            sd.highScore = highScore;

            if (FileManager.WriteToFile("HighScore.dat",sd.ToJson()))
            {
                Debug.Log("Save succesful");
                Debug.Log("The high score is: " + highScore);
            }
        }

        gameOverScreen.Setup(time);
    }

    public void DebugMenu()
    {
        Time.timeScale = 0;
        debugScreen.Setup();
    }
}
