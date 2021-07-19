using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    public float timeOffset = 0.4f;
   // public float distanceBetweenTiles = 0.0f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);
    private float tileLength;

    private GameObject previousTile;

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition = referenceObject.transform.position;

        previousTile = referenceObject;

        startTime = Time.time;
        tileLength= referenceObject.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time - startTime > timeOffset)
        /*
        if (Camera.main.transform.position.z+Camera.main.farClipPlane > previousTilePosition.z) //farClipPlane is too far
        {

            direction = mainDirection;

            Vector3 spawnPos = previousTilePosition + tileLength * direction;
           // startTime = Time.time;
            Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
        }
        */
        Vector3 checkVector= Camera.main.WorldToViewportPoint(previousTilePosition);

        if (checkVector.x>0&& checkVector.x < 1&& checkVector.y > 0 && checkVector.y < 1 && checkVector.z>0) //farClipPlane is too far
        {

            direction = mainDirection;

            Vector3 spawnPos = previousTilePosition + tileLength * direction;
            Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
        }

        /*
        if (previousTile.GetComponent<Renderer>().isVisible)
        {
            direction = mainDirection;

            Vector3 spawnPos = previousTile.transform.position + tileLength * direction;
            previousTile= Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
        }
        */
    }

}
