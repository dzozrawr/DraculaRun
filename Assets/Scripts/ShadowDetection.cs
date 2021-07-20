using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetection : MonoBehaviour
{
    public GameObject directionalLight;
    private Vector3 lightDirection;
    private int layerMask;

    private PlayerController playerController;
    public float damagePerSecond = 5;
    // Start is called before the first frame update
    void Start()
    {
        lightDirection = -directionalLight.transform.forward;
        layerMask = (1 << 3) | (1 << 6);    //hard coded  Player layer number=3 and Tile layer number=6
        layerMask = ~layerMask;

        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hit;
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
            }
            else playerController.doDamage(damagePerSecond * Time.deltaTime);  //taking damage from sunlight only if umbrella isnt available
        }
        else    //if the player is in the shadow
        {
            if (playerController.IsUmbrellaAvailable)
            {
                playerController.getPlayerUmbrella().SetActive(false);
            }
        }
    }
}
