using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] music, sfx;
    public AudioSource musicSource, ottoSource, clockSource, currencySource, cardSource;
    public AudioSource[] characterSource;

    private void Awake()
    {
        BlackBoard.audioM = this;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlayOtto(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            ottoSource.clip = s.clip;
            ottoSource.Play();
        }
    }

    public void PlayClock(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            clockSource.clip = s.clip;
            clockSource.Play();
        }
    }

    public void PlayCurrency(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            currencySource.clip = s.clip;
            currencySource.Play();
        }
    }

    public void PlayCard(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            cardSource.clip = s.clip;
            cardSource.Play();
        }
    }

    public void PlayCharacter(string name, int character)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            characterSource[character].clip = s.clip;
            characterSource[character].Play();
        }
    }
}
