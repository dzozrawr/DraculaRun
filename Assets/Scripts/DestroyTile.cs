using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    private GameObject player;
    private int distanceInTilesForDeletion = 2;
    private float tileLength;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tileLength = transform.localScale.z;
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < player.transform.position.z)     //if the tile is behind the player
        {
            if ((player.transform.position.z - transform.position.z) > tileLength * distanceInTilesForDeletion)     //if the desired distance for deletion is achieved
            {
                Destroy(gameObject);    //destroy this tile
            }
        }

    }
}
