using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioMain;
    [SerializeField] AudioSource audioShop;
    [SerializeField] List<AudioSource> audioAmbient;

    bool muteAmbient = false;
    bool muteMusic = false;

    void Start()
    {
        audioMain.Play();
    }

    public void ToggleMainMusic()
    {
        muteMusic = !muteMusic;

        audioMain.mute = muteMusic;
        
        if (audioShop.isPlaying)
        {
            audioShop.Stop();
        }
        else
        {
            audioShop.Play();
        }
    }

    public void ToggleAmbient()
    {
        muteAmbient = !muteAmbient;

        foreach (AudioSource source in audioAmbient)
        {
            source.mute = muteAmbient;
        }
    }

    public void PlayClip(AudioClip clip)
    {

    }
}
