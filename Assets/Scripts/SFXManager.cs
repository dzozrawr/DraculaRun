using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour     //class made, because additional sound effects will be added hopefully
{
    public static AudioClip transformationSound;
    public static AudioSource  audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        transformationSound = Resources.Load<AudioClip>("transformation");


        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "transformation":
                audioSrc.PlayOneShot(transformationSound);
                break;
        }
    }
}
