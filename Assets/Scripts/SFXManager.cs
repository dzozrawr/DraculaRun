using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour     //class made, because additional sound effects will be added hopefully
{                                           //this class is actually for non looping sfx
    public static AudioClip transformationSound, umbrellaPickupSound;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        transformationSound = Resources.Load<AudioClip>("transformation");
        umbrellaPickupSound= Resources.Load<AudioClip>("umbrellaPickup");

        audioSrc = GetComponent<AudioSource>();        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "transformation":
                audioSrc.PlayOneShot(transformationSound,0.75f);
                break;
            case "umbrellaPickup":
                audioSrc.PlayOneShot(umbrellaPickupSound);
                break;
        }
    }
}