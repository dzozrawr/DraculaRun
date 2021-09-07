using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //public GameObject tileToSpawn;
    public GameObject[] tilesToSpawn;   //array is sorted from the least difficult tiles to most difficult
    public GameObject previousTile;
    private Vector3 previousTilePosition;
    private Vector3 direction = new Vector3(0, 0, 1);   // z direction (forward direction)
    private float tileLength;


    private GameObject nextTile;
    private Vector3 spawnPos;


    private int difficultyTileRange;    //how many tiles are considered to be spawned from the array
    private int difficultyIndexOffset=0;
    private int difficultyIndexCeiling;

    private const int tilesToSpawnToIncreaseDifficulty=2;
    private int tilesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {

        previousTilePosition = previousTile.transform.position;
        tileLength = previousTile.GetComponent<MeshRenderer>().bounds.size.z;

        difficultyTileRange=tilesToSpawn.Length/3;
        difficultyIndexCeiling= tilesToSpawn.Length- difficultyTileRange-1;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 checkVector= Camera.main.WorldToViewportPoint(previousTilePosition);
        // if (checkVector.x>0&& checkVector.x < 1&& checkVector.y > 0 && checkVector.y < 1 && checkVector.z>0) //previous tile detection strategy (by the camera)
        if (Camera.main.transform.position.z + Camera.main.farClipPlane > previousTilePosition.z) //if far clip plane is in front of the previous tile, generate a new tile
        {
            //nextTile = tilesToSpawn[Random.Range(0, tilesToSpawn.Length)];
            
            nextTile = tilesToSpawn[Random.Range(difficultyIndexOffset, difficultyIndexOffset+ difficultyTileRange+1)];
            spawnPos = previousTilePosition + tileLength * direction;
            Instantiate(nextTile, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
            tileLength = nextTile.GetComponent<Renderer>().bounds.size.z;
         //   Debug.Log(nextTile.name);

            tilesSpawned++;
            if (tilesSpawned == tilesToSpawnToIncreaseDifficulty)
            {
                tilesSpawned = 0;
                if(difficultyIndexOffset< difficultyIndexCeiling)//increase difficulty
                {
                  //  Debug.Log("Difficulty increased");
                    difficultyIndexOffset++;
                }
                
            }
        }

    }

}