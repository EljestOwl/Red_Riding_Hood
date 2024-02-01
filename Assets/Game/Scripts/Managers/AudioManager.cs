using System;
using System.Collections;
using UnityEngine;

public class AudioMangagerScript : MonoBehaviour
{
    #region Variables

    public static AudioMangagerScript instance;
    public Sound[] musicSounds, sfxSounds, playerSounds, enemySounds;

    private float musicVolume;
    private float sfxVolume;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Load global volume from PlayerPrefs
        musicVolume = PlayerPrefs.GetFloat("GlobalMusicVolume", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("GlobalSFXVolume", 0.75f);

        // Create all the AudioSources
        CreateAudioSources(musicSounds, musicVolume);
        CreateAudioSources(sfxSounds, sfxVolume);
        CreateAudioSources(playerSounds, sfxVolume);
        CreateAudioSources(enemySounds, sfxVolume);
    }

    private void CreateAudioSources(Sound[] sounds, float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    public void PlaySound(string name)
    {
        Sound s;

        // Search through all arrays and check after each if it is still null, if so: continue, if not: break loop.
        while (true)
        {
            s = Array.Find(musicSounds, x => x.clip_name == name);
            if (s != null) break;
            s = Array.Find(sfxSounds, x => x.clip_name == name);
            if (s != null) break;
            s = Array.Find(playerSounds, x => x.clip_name == name);
            if (s != null) break;
            s = Array.Find(enemySounds, x => x.clip_name == name);
            if (s != null) break;
        }

        // If still null, send error & exit method
        if (s == null)
        {
            Debug.Log("Sound: " + name + ", Not Found!");
            return;
        }

        s.source.Play();
    }

    public IEnumerator PlaySoundWithInterval(string name, float seconds)
    {
        Sound s = Array.Find(musicSounds, x => x.clip_name == name);

        if (s == null)
        {
            Debug.Log("Sound: " + name + ", Not Found!");
        }

        s.source.Play();
        
        yield return new WaitForSeconds(seconds);
    }

    public void MusicVolumeUpdate()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        foreach (Sound s in musicSounds)
        {
            s.source.volume = musicSounds[0].volume * musicVolume;
        }
    }

    public void SFXVolumeUpdate()
    {
        sfxVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in sfxSounds)
        {
            s.source.volume = s.volume * sfxVolume;
        }
        foreach (Sound s in playerSounds)
        {
            s.source.volume = s.volume * sfxVolume;
        }
        foreach (Sound s in enemySounds)
        {
            s.source.volume = s.volume * sfxVolume;
        }

        // play an effect so user can her effect volume
        sfxSounds[0].source.Play(); 
    }
}
