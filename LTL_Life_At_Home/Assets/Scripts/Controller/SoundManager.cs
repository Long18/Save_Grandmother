using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource loopAudio, shotAudio, BGaudio;

    public string[] oneShotName;
    public string[] loopName;
    public AudioClip[] oneShotClip;
    public AudioClip[] loopClip;
    public AudioClip BGClip;

    public bool soundMute, musicMute;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        soundMute = false; musicMute = false;
    }

    private void Update()
    {
        if (musicMute && soundMute)
        {
            AudioSource[] objs = GameObject.FindObjectsOfType<AudioSource>();
            foreach(AudioSource au in objs)
            {
                au.volume = 0;
            }
        }
        else if (!musicMute && !soundMute)
        {
            AudioSource[] objs = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource au in objs)
            {
                au.volume = 1;
            }
        }
    }
    public void PlayBGSound()
    {
        BGaudio.clip = BGClip;
        BGaudio.Play();
    }
    public void StopBGSound()
    {
        BGaudio.Stop();
    }

    public void PlayOneShotClip(string name)
    {
        if (!musicMute)
        {
            shotAudio.PlayOneShot(oneShotClip[Array.IndexOf(oneShotName, name)]);
        }
    }

    public void PlayLoopClip(string name)
    {
        if (!musicMute)
        {
            GameObject obj = new GameObject();
            Transform childT = GameController.instance.Pool.Find(name);
            if (childT == null)
            {
                obj.name = name;
                obj.transform.SetParent(GameController.instance.Pool);
                obj.AddComponent<AudioSource>();
                AudioSource aud = obj.GetComponent<AudioSource>();
                obj.tag = "music";
                aud.loop = true;
                aud.playOnAwake = false;
                int ind = Array.IndexOf(loopName, name);
                aud.clip = loopClip[ind];
                aud.Play();
            }
            else
            {
                childT.gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
    public void StopLoopClip(string name)
    {
        if (!musicMute)
        {
            GameObject obj = GameController.instance.Pool.Find(name).gameObject;
            obj.GetComponent<AudioSource>().Stop();
        }
    }
}
