using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] arrowSounds;
    public Sound[] playerSounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound aS in arrowSounds)
        {
            aS.source = gameObject.AddComponent<AudioSource>();
            aS.source.clip = aS.clip;

            aS.source.volume = aS.volume;
            aS.source.pitch = aS.pitch;
            aS.source.loop = aS.loop;
        }
        foreach (Sound pS in playerSounds)
        {
            pS.source = gameObject.AddComponent<AudioSource>();
            pS.source.clip = pS.clip;

            pS.source.volume = pS.volume;
            pS.source.pitch = pS.pitch;
            pS.source.loop = pS.loop;
        }

    }
    private void Start()
    {
        PlaySound("GrasslandTheme");
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }               
        s.source.Play();
    }
    public void PlayArrowSound(string name)
    {
        Sound aS = Array.Find(sounds, sound => sound.name == name);
        if (aS == null)
        {
            return;
        }
        aS.source.Play();
    }

    public void PlayPlayerSound(string name)
    {
        Sound pS = Array.Find(sounds, sound => sound.name == name);
        if (pS == null)
        {
            return;
        }
        pS.source.Play();
    }

}
