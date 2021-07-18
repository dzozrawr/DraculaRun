using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetection : MonoBehaviour
{
    public GameObject directionalLight;
    private Vector3 lightDirection;
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        lightDirection = -directionalLight.transform.forward;
        layerMask = 1 << 3;    //hard coded  Player layer number=3
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {

        bool hit = Physics.Raycast(transform.position, lightDirection, Mathf.Infinity, layerMask);

        if (hit) Debug.Log("Player is in shadow!"); else Debug.Log("Player is in sunlight!");
    }
}
