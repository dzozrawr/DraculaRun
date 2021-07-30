using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //public GameObject tileToSpawn;
    public GameObject[] tilesToSpawn;
    public GameObject previousTile;
    private Vector3 previousTilePosition;
    private Vector3 direction = new Vector3(0, 0, 1);   // z direction (forward direction)
    private float tileLength;


    private GameObject nextTile;
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {

        previousTilePosition = previousTile.transform.position;
        tileLength = previousTile.GetComponent<MeshRenderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 checkVector= Camera.main.WorldToViewportPoint(previousTilePosition);
        // if (checkVector.x>0&& checkVector.x < 1&& checkVector.y > 0 && checkVector.y < 1 && checkVector.z>0) //previous tile detection strategy (by the camera)
        if (Camera.main.transform.position.z + Camera.main.farClipPlane > previousTilePosition.z) //if far clip plane is in front of the previous tile, generate a new tile
        {
            nextTile = tilesToSpawn[Random.Range(0, tilesToSpawn.Length)];
            spawnPos = previousTilePosition + tileLength * direction;
            Instantiate(nextTile, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
            tileLength = nextTile.GetComponent<Renderer>().bounds.size.z;
        }

    }

}