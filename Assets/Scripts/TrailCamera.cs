using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{

    public Transform target;
    public float trailDistance = 5.0f;
    public float heightOffset = 3.0f;
   // public float cameraDelay = 0.02f;

    private void Start()
    {
        transform.LookAt(target.transform); //camera looks at player (rotation)
    }

    void Update()
    {
        //Vector3 followPos = target.position - target.forward * trailDistance;

        transform.position = new Vector3(transform.position.x, target.position.y+ heightOffset, target.position.z - trailDistance); //camera centered while following at a certain distance and following the player if he jumps

      //  followPos.y += heightOffset;
        //transform.position += (followPos - transform.position) * cameraDelay;
    }
}
