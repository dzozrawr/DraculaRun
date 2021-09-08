using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject[] tilesToSpawn;   //array is sorted from the least difficult tiles to most difficult
    public GameObject previousTile;
    private Vector3 previousTilePosition;
    private Vector3 direction = new Vector3(0, 0, 1);   // z direction (forward direction)
    private float tileLength;


    private GameObject nextTile;
    private Vector3 spawnPos;


    private List<int> tileNumberSet;      //responsible for generating diverse tiles in random order
    private int randomN;
    // Start is called before the first frame update
    void Start()
    {

        previousTilePosition = previousTile.transform.position;
        tileLength = previousTile.GetComponent<MeshRenderer>().bounds.size.z;

        tileNumberSet = new List<int>();
        instantiateTileNumberSet();
    }

    private void instantiateTileNumberSet()
    {
        for (int i = 0; i < tilesToSpawn.Length; i++)
        {
            tileNumberSet.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Camera.main.transform.position.z + Camera.main.farClipPlane > previousTilePosition.z) //if far clip plane is in front of the previous tile, generate a new tile
        {
            
            randomN = Random.Range(0, tileNumberSet.Count);           

            nextTile = tilesToSpawn[tileNumberSet[randomN]];    //a random tile is chosen from the set, it will not be chosen again until all tiles have been chosen
            tileNumberSet.RemoveAt(randomN);

            spawnPos = previousTilePosition + tileLength * direction;
            Instantiate(nextTile, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
            tileLength = nextTile.GetComponent<Renderer>().bounds.size.z;

            if (tileNumberSet.Count == 0)   //when all tiles have been chosen once, the set refills with tile numbers and the process repeats
            {
                instantiateTileNumberSet();
            }
        }

    }

}