using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    public GameObject player;
    public Slider charSpeedSlider;
    public Text charSpeedSliderText;

    private void Start()
    {
        charSpeedSlider.value = player.GetComponent<PlayerController>().speed;
        charSpeedSliderText.text = charSpeedSlider.value.ToString("F");
    }
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void exitDebug()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void setCharacterSpeed(float speed)
    {
        player.GetComponent<PlayerController>().speed= speed;
        charSpeedSliderText.text = charSpeedSlider.value.ToString("F");
        //  Debug.Log(speed);
    }
}
