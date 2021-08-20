﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Awake()
    {
        PlayerPrefs.SetInt("numberOfGameSceneLoads", 0);
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
