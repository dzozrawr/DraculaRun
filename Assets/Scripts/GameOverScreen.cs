using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText,hiScoreText;
    // Start is called before the first frame update
    public void Setup(float score,float hiScore)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString("F") + " sec";
        hiScoreText.text= hiScore.ToString("F") + " sec";
    }

    public void RestartButton()
    {
     
        SceneManager.LoadScene("Game");
        
    }

    public void MainMenuButton()
    {

        SceneManager.LoadScene("MainMenu");

    }

    public void QuitButton()
    {
        
        Application.Quit();

    }


}
