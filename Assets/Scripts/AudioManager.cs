using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip engineDriving;
    public AudioClip engineIdle;
    public AudioClip shellExplosion;
    public AudioClip shotCharging;
    public AudioClip shotFiring;
    public AudioClip tankExplosion;

    public static AudioManager Instance
    {
        get { 
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance; 
        }
    }
    private void Awake()
    {
        // Ensure there is only one instance of this script
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameObject persistent between scenes
        }
        else
        {
            Destroy(gameObject); // If another instance already exists, destroy this one
        }
    }
}
