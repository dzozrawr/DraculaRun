using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    // Start is called before the first frame update
    public void Setup(float score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString("F") + " sec";
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
       // SceneManager.LoadScene("MainMenu");   to be implemented
    }
}
