using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject playerUmbrella;

    public Toggle deathToggleButton;

    public Slider playerMaxHPSlider;
    public Text playerMaxHPSliderText;

    public Slider charSpeedSlider;
    public Text charSpeedSliderText;

    public Slider charTurnSpeedSlider;
    public Text charTurnSpeedSliderText;

    public Slider sunDPSSlider;
    public Text sunDPSSliderText;

    public Slider shadowHPSSlider;
    public Text shadowHPSSliderText;

    public Slider umbrellaBaseHPSlider;
    public Text umbrellaBaseHPSliderText;

    public Slider cameraFarClipSlider;
    public Text cameraFarClipSliderText;

    public Slider charRotAngleLimitSlider;
    public Text charRotAngleLimitSliderText;

    public Slider charRotSpeedSlider;
    public Text charRotSpeedSliderText;

    public Slider musicVolumeSlider;
    public Text musicVolumeSliderText;

    private void Start()
    {
        deathToggleButton.isOn = player.GetComponent<PlayerController>().isDeathDisabled;

        playerMaxHPSlider.value = player.GetComponent<PlayerController>().getMaxHP();
        playerMaxHPSliderText.text = playerMaxHPSlider.value.ToString("F");

        charSpeedSlider.value = player.GetComponent<PlayerController>().speed;
        charSpeedSliderText.text = charSpeedSlider.value.ToString("F");

        charTurnSpeedSlider.value = player.GetComponent<PlayerController>().turnSpeed;
        charTurnSpeedSliderText.text = charTurnSpeedSlider.value.ToString("F");

        sunDPSSlider.value = player.GetComponent<ShadowDetection>().damagePerSecond;
        sunDPSSliderText.text = sunDPSSlider.value.ToString("F");

        shadowHPSSlider.value = player.GetComponent<ShadowDetection>().healthRegenPerSecond;
        shadowHPSSliderText.text = shadowHPSSlider.value.ToString("F");

        umbrellaBaseHPSlider.value = playerUmbrella.GetComponent<UmbrellaHP>().baseHP;
        umbrellaBaseHPSliderText.text = umbrellaBaseHPSlider.value.ToString("F");

        cameraFarClipSlider.value = Camera.main.farClipPlane;
        cameraFarClipSliderText.text = cameraFarClipSlider.value.ToString("F");

        charRotAngleLimitSlider.value = player.GetComponent<PlayerController>().turnAngleLimit;
        charRotAngleLimitSliderText.text = charRotAngleLimitSlider.value.ToString("F");

        charRotSpeedSlider.value = player.GetComponent<PlayerController>().rotationSpeed;
        charRotSpeedSliderText.text = charRotSpeedSlider.value.ToString("F");

        musicVolumeSlider.value = SoundManagerScript.backgroundAudioSrc.volume;
        musicVolumeSliderText.text = musicVolumeSlider.value.ToString("F");
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

    public void toggleDeath(bool isDeathDisabled)
    {
        player.GetComponent<PlayerController>().isDeathDisabled = isDeathDisabled;
    }

    public void setPlayerMaxHP(float maxHP)
    {
        player.GetComponent<PlayerController>().setMaxHP(maxHP);
        playerMaxHPSliderText.text = maxHP.ToString("F");
    }
    public void setCharacterSpeed(float speed)
    {
        player.GetComponent<PlayerController>().speed= speed;
        charSpeedSliderText.text = charSpeedSlider.value.ToString("F");
    }

    public void setCharacterTurnSpeed(float turnSpeed)
    {
        player.GetComponent<PlayerController>().turnSpeed = turnSpeed;
        charTurnSpeedSliderText.text = charTurnSpeedSlider.value.ToString("F");
    }



    public void setSunDPS(float sunDPS)
    {
        player.GetComponent<ShadowDetection>().damagePerSecond = sunDPS;
        sunDPSSliderText.text=sunDPSSlider.value.ToString("F");
    }

    public void setShadowHPS(float shadowHPS)
    {
        player.GetComponent<ShadowDetection>().healthRegenPerSecond = shadowHPS;
        shadowHPSSliderText.text = shadowHPSSlider.value.ToString("F");
    }

    public void setUmbrellaBaseHP(float baseHP)
    {
        playerUmbrella.GetComponent<UmbrellaHP>().baseHP = baseHP;
        umbrellaBaseHPSliderText.text = umbrellaBaseHPSlider.value.ToString("F");
    }

    public void setCameraFarClip(float farClip)
    {
        Camera.main.farClipPlane = farClip;
        cameraFarClipSliderText.text = farClip.ToString("F");
    }

    public void setCharRotAngleLimit(float angleLimit)
    {
        player.GetComponent<PlayerController>().turnAngleLimit = angleLimit;
        charRotAngleLimitSliderText.text = angleLimit.ToString("F");
    }

    public void setCharRotSpeed(float rotSpeed)
    {
        player.GetComponent<PlayerController>().rotationSpeed = rotSpeed;
        charRotSpeedSliderText.text = rotSpeed.ToString("F");
    }

    public void setMusicVolume(float musicVolume)
    {
        SoundManagerScript.backgroundAudioSrc.volume = musicVolume;
        musicVolumeSliderText.text= musicVolume.ToString("F");
    }

}
