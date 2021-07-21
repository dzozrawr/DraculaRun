using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{

    public Transform target;
    private float trailDistance = 5.0f;  //was public
    private float heightOffset = 3.0f;   //was public

    private void Start()
    {
        heightOffset = transform.position.y - target.position.y;    //initialized here, because it makes it easier to configure the camera in the editor
        trailDistance = target.position.z - transform.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + heightOffset, target.position.z - trailDistance); 
    }
}
