using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;
    //public static AudioManager instance;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.Clip;
            sound.source.volume = sound.Vulume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
        }
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public IEnumerator FadeOutSound(string name, float fadeTime)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);

        Debug.Log("starting to fade audio");


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            yield break;
        }

        Debug.Log("Fading audio!");

        float startVolume = s.source.volume;

        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        s.source.Stop();
        s.source.volume = startVolume;
    }

}
