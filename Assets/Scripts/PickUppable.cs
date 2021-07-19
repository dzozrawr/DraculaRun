using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().pickUpUmbrella(); //umbrella becomes available and gains +baseHP
            //effect, could be made abstract in case of multiple power ups
            //play sound maybe
            Destroy(gameObject);
        }
    }
}
