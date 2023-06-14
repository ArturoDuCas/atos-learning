using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController instance;
    public AudioClip defaultBackgroundMusic; 
    public AudioClip inGameBackgroundMusic;

    

    private AudioSource audioSource;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

    }

    void Start() {
        PlayBackgroundMusic(defaultBackgroundMusic);
    }

    public void PlayBackgroundMusic(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    
}
