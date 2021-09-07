using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen;
    public HighScoreScreen highScoreScreen;
    public TimePassed timePassed;
    public DebugScreen debugScreen;

    private float highScore;

    [SerializeField] private float tutorialLifetimeInSec = 3;
    public GameObject tutorialUIElements;

    private void Awake()
    {
        int n_runs=PlayerPrefs.GetInt("numberOfGameSceneLoads");
        if (n_runs == 0)
        {
            tutorialUIElements.SetActive(true); //tutorial is disabled, it is enabled only on the first run and then disabled in the next runs by default (through the editor)
            PlayerPrefs.SetInt("numberOfGameSceneLoads", 1);
        }

        Time.timeScale = 1f;    //if the game was paused

        if (FileManager.LoadFromFile("HighScore.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            highScore = sd.highScore;
           // Debug.Log("Load complete");
        }
        else
        {
            highScore = 0f;
        }
       // Debug.Log("The high score is: " + highScore);
    }

    private void Update()
    {
       
        if (tutorialLifetimeInSec <= 0)
        {
            tutorialUIElements.SetActive(false);
        }
        else
        {
            tutorialLifetimeInSec -= Time.deltaTime;
        }
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        float time = timePassed.GetComponent<TimePassed>().getTime();

        if (time > highScore)       //if the score is better than previous high score, save the new high score
        {
            highScore = time;
            SaveData sd = new SaveData();
            sd.highScore = highScore;

            if (FileManager.WriteToFile("HighScore.dat",sd.ToJson()))
            {
                Debug.Log("Save succesful");
                Debug.Log("The high score is: " + highScore);
            }
            highScoreScreen.Setup(time);
        }
        else
        {
            gameOverScreen.Setup(time, highScore);
        }
    
    }

    public void DebugMenu()
    {
        Time.timeScale = 0;
        debugScreen.Setup();
    }
}
