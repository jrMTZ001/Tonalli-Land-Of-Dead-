using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource menuMusic, bossMusic;
    public AudioSource[] levelTracks;
    
    void StopMusic()
    {
        menuMusic.Stop();
        bossMusic.Stop();
        
        foreach(AudioSource track in levelTracks)
        {
            track.Stop();
        }
    }
    public void PlayMenuMusic()
    {   
        StopMusic();
        menuMusic.Play();
    }
}
