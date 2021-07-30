using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip backgroundMusic;
    public static AudioSource backgroundAudioSrc;
    // Start is called before the first frame update
    void Start()
    {

        backgroundMusic = Resources.Load<AudioClip>("backgroundMusic");
        backgroundAudioSrc = GetComponent<AudioSource>();

        backgroundAudioSrc.loop = true;
        backgroundAudioSrc.clip = backgroundMusic;
        backgroundAudioSrc.volume = 0.2f;
        backgroundAudioSrc.Play();
    }

}