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
        layerMask = 1 << 3;    //hard coded  Player layer number=3
        layerMask = ~layerMask;

        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hit;
        if (playerController.IsVampireForm)
        {
            hit = Physics.Raycast(playerController.vampire.transform.position, lightDirection, Mathf.Infinity, layerMask);
        }
        else
        {
            hit = Physics.Raycast(playerController.bat.transform.position, lightDirection, Mathf.Infinity, layerMask);
        }
        

        if (!hit)
        {
            playerController.doDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
