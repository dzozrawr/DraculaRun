using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetection : MonoBehaviour
{
    public GameObject directionalLight;
    private Vector3 lightDirection;
    private int layerMask;

    private PlayerController playerController;
    public float damagePerSecond = 5, healthRegenPerSecond = 5;
    private bool hit; //rayCast variable
    public GameObject healthBarGlow;    //the health bar glows when the player is in the sun


    // private AudioSource sizzlingAudioSrc; //sizzling while in the sunlight was here previously



    private void Awake()
    {
        lightDirection = -directionalLight.transform.forward;
        layerMask = (1 << 3) | (1 << 6);    //hard coded  Player layer number=3 and Tile layer number=6
        layerMask = ~layerMask;
    }
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        healthBarGlow.SetActive(false);
       // sizzlingAudioSrc = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Time.timeScale == 0 || !playerController.IsScriptStarted)  //if paused do nothing OR if player isnt instantiated (hp=0) do nothing
        {
            //if (sizzlingAudioSrc.isPlaying) sizzlingAudioSrc.Stop();
            return;
        }
       

        if (playerController.IsVampireForm)
        {
            hit = Physics.Raycast(playerController.vampire.transform.position, lightDirection, Mathf.Infinity, layerMask);  //casting an infinite ray from player towards the lights direction
        }
        else
        {
            hit = Physics.Raycast(playerController.bat.transform.position, lightDirection, Mathf.Infinity, layerMask);
        }


        if (!hit)   //if the player is in the sunlight
        {
            if (playerController.IsUmbrellaAvailable)
            {
                playerController.getPlayerUmbrella().SetActive(true);
                playerController.getPlayerUmbrella().GetComponent<UmbrellaHP>().doDamage(damagePerSecond * Time.deltaTime); //do damage to umbrella
                healthBarGlow.SetActive(false);
               // if (sizzlingAudioSrc.isPlaying) sizzlingAudioSrc.Stop();
            }
            else
            {
                playerController.doDamage(damagePerSecond * Time.deltaTime);  //taking damage from sunlight only if umbrella isnt available
                healthBarGlow.SetActive(true);
               // if (!sizzlingAudioSrc.isPlaying) sizzlingAudioSrc.Play();
            }
        }
        else    //if the player is in the shadow
        {
            healthBarGlow.SetActive(false);
           // if (sizzlingAudioSrc.isPlaying) sizzlingAudioSrc.Stop();
                playerController.addHP(healthRegenPerSecond * Time.deltaTime);  //regenerating hp in the shadow
            if (playerController.IsUmbrellaAvailable)
            {
                playerController.getPlayerUmbrella().SetActive(false);
            }
        }
    }
}