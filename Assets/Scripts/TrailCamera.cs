using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{

    public Transform target;
    private float trailDistance = 5.0f;  //was public
    private float heightOffset = 3.0f;   //was public
                                         // public float cameraDelay = 0.02f;

    private void Start()
    {
        heightOffset = transform.position.y - target.position.y;
        trailDistance = target.position.z - transform.position.z;
        // transform.LookAt(target.transform); //camera looks at player (rotation)
    }

    void LateUpdate()
    {

        transform.position = new Vector3(transform.position.x, target.position.y + heightOffset, target.position.z - trailDistance); //camera centered while following at a certain distance and following the player if he jumps

    }
}
