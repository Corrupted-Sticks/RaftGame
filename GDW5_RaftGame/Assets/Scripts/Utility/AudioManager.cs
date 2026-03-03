using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource audioMain;
    [SerializeField] AudioSource audioShop;
    [SerializeField] List<AudioSource> audioSFX;
    [SerializeField] List<AudioSource> audioAmbient;

    bool muteAmbient = false;
    bool muteMusic = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

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

    public void PlaySFX(int num)
    {
        audioSFX[num].Play();
    }

    public void PlayClip(AudioClip clip)
    {

    }
}
