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
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addHP(100); //added this, because otherwise the HP is 0 in the beginning and the game pauses
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
   {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        Time.timeScale = 1f;    //for safety
    }
}
