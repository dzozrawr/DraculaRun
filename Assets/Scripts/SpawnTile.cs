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
    

    // Start is called before the first frame update
    void Start()
    {
        
        previousTilePosition = previousTile.transform.position;
        tileLength = previousTile.GetComponent<MeshRenderer>().bounds.size.z;
        Debug.Log(tileLength);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 checkVector= Camera.main.WorldToViewportPoint(previousTilePosition);

        if (checkVector.x>0&& checkVector.x < 1&& checkVector.y > 0 && checkVector.y < 1 && checkVector.z>0) //if previous tile is seen by the camera, make a new one in front of it
        {
            GameObject nextTile = tilesToSpawn[Random.Range(0, tilesToSpawn.Length)];            
            Vector3 spawnPos = previousTilePosition + tileLength * direction;
            Instantiate(nextTile, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;
            // tileLength = nextTile.transform.localScale.z;
            tileLength = nextTile.GetComponent<Renderer>().bounds.size.z;
            Debug.Log(tileLength);
        }

    }

}
