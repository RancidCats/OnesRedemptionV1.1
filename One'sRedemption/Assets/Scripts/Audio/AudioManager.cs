using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            if (s.playOnAwake)
                s.source.Play();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sonido => sonido.name == name);
        if (s != null)
            s.source.Play();
        else
            Debug.LogError("El sonido " + name + " no existe.");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sonido => sonido.name == name);
        if (s != null)
            s.source.Stop();
        else
            Debug.LogError("El sonido " + name + " no existe.");
    }
}
