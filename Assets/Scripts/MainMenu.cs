using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private float highScore;
    [SerializeField] private Text highScoreTxt;
    private void Awake()
    {
        PlayerPrefs.SetInt("numberOfGameSceneLoads", 0);    //helping variable for tutorial, because tutorial should be shown only once

        if (FileManager.LoadFromFile("HighScore.dat", out var json))    //reading high score value
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

        highScoreTxt.text = highScore.ToString("F")+" sec";
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
