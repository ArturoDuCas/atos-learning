using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundsController : MonoBehaviour
{
    public static BackgroundSoundsController instance;
    public AudioClip clickSound; 

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

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            audioSource.Play();
        }
    }
}
