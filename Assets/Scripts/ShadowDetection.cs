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

    [SerializeField] private ParticleSystem draculaSmoke, batSmoke;

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
    }


    void Update()
    {
        if (playerController.is_gameOver())
        {
            if (draculaSmoke.isPlaying) draculaSmoke.Stop();
            if (batSmoke.isPlaying) batSmoke.Stop();            //this is to prevent smoke playing during the death animations
        }

        if (Time.timeScale == 0)  //if paused do nothing
        {
           
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
            if (playerController.IsUmbrellaAvailable)   //if the player is hit by sunlight and has an umbrella
            {
                playerController.getPlayerUmbrella().SetActive(true);
                playerController.getPlayerUmbrella().GetComponent<UmbrellaHP>().doDamage(damagePerSecond * Time.deltaTime); //do damage to umbrella
                healthBarGlow.SetActive(false);

                if (draculaSmoke.isPlaying) draculaSmoke.Stop();
                if (batSmoke.isPlaying) batSmoke.Stop();

            }
            else     //taking damage from sunlight only if umbrella isnt available
            {
                if (playerController.IsVampireForm)
                {
                    if (!draculaSmoke.isPlaying) draculaSmoke.Play();//play smoke particle effect that represents frying in the sun
                }
                else
                {
                    if (!batSmoke.isPlaying) batSmoke.Play();//play smoke particle effect that represents frying in the sun
                }
                playerController.doDamage(damagePerSecond * Time.deltaTime);
                healthBarGlow.SetActive(true);
          
            }
        }
        else    //if the player is in the shadow
        {
            if (draculaSmoke.isPlaying) draculaSmoke.Stop();
            if (batSmoke.isPlaying) batSmoke.Stop();

            healthBarGlow.SetActive(false);
            playerController.addHP(healthRegenPerSecond * Time.deltaTime);  //regenerating hp in the shadow
            if (playerController.IsUmbrellaAvailable)
            {
                playerController.getPlayerUmbrella().SetActive(false);
            }
        }
    }
}