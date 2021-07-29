using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip fryingSound,backgroundMusic;
    public static AudioSource backgroundAudioSrc, audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        //fryingSound = Resources.Load<AudioClip>("frying");

       // backgroundMusic = Resources.Load<AudioClip>("backgroundMusic");
      //  backgroundAudioSrc = GetComponent<AudioSource>();

     //   backgroundAudioSrc.loop = true;
      //  backgroundAudioSrc.clip = backgroundMusic;
     //   backgroundAudioSrc.Play();
        //audioSrc.volume;
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "frying":
                audioSrc.PlayOneShot(fryingSound);
                break;
        }
    }
}
