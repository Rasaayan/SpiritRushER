using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public GameManager gameManagerObj;

    public static AudioManager Instance;

    public DistanceDisplay distanceObj;

    public AudioSource audioSource;
    public List<SoundClip> soundClips = new List<SoundClip>();

    double distanceTracker = 0.0;
    bool ambientActive = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(string name)
    {
        foreach (SoundClip sc in soundClips)
        {
            if (sc.name == name)
            {
                audioSource.PlayOneShot(sc.audioClip);
            }
        }
    }

    public void StopClip(string name )
    {
        foreach (SoundClip sc in soundClips)
        {
            if (sc.name == name)
            {
                audioSource.Stop();
            }
        }
    }

    [System.Serializable]
    public class SoundClip
    {
        public string name;
        public AudioClip audioClip;

    }

    public void Ambience()
    {        
        if (gameManagerObj.currentGameLevel == 1)
        {
            PlayClip("Thunder");
        }
        if (gameManagerObj.currentGameLevel == 2)
        {
            
        }
        if (gameManagerObj.currentGameLevel == 3)
        {

        }
    }

    public void Update()
    {
        distanceTracker = Math.Round(distanceObj.distanceTravelled);
        if(distanceTracker % 300 == 0 && distanceTracker != 0 && ambientActive == false)
        {
            Ambience();
            ambientActive = true;
        }
        if(distanceTracker % 300 == 5)
        {
            ambientActive = false;
        }

    }
}
