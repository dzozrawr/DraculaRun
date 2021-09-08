using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text pointsText;
    [SerializeField] private GameObject leftConfetti, bottomConfetti, rightConfetti, topConfetti;




    // Start is called before the first frame update
    public void Setup(float score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString("F") + " sec";

        leftConfetti.GetComponent<ParticleSystem>().Play();
        bottomConfetti.GetComponent<ParticleSystem>().Play();
        rightConfetti.GetComponent<ParticleSystem>().Play();
        topConfetti.GetComponent<ParticleSystem>().Play();

        SFXManager.PlaySound("victory");
    }


    public void RestartButton()
    {

        //  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addHP(100); //added this, because otherwise the HP is 0 in the beginning and the game pauses
        SceneManager.LoadScene("Game");

    }

    public void QuitButton()
    {

        //  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().addHP(100); //added this, because otherwise the HP is 0 in the beginning and the game pauses
        Application.Quit();

    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
