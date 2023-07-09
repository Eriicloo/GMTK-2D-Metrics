using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] music, sounds;
    [HideInInspector] public bool isPlayingMainMenuMusic;

    public AudioSource musicSource, soundsSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        /*
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        */
    }
        public void PlayMusic(string name)
    {
        Sound song = Array.Find(music, x => x.name == name);

        if (music == null)
        {
            Debug.Log("Song Not Found");
        }

        else
        {
            isPlayingMainMenuMusic = name == "MainMenu";
            musicSource.clip = song.clip;
            musicSource.Play();
        }
    }

    public void PlaySounds(string name)
    {
        Sound sound = Array.Find(sounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            soundsSource.PlayOneShot(sound.clip);
        }
    }
}
