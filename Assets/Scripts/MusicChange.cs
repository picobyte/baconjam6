﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (AudioSource))]
public class MusicChange : MonoBehaviour
{
    private static MusicChange instance;
    
    [SerializeField]
    private AudioSource source1;
    [SerializeField]
    private AudioSource source2;
    
    private AudioSource primary;
    private AudioSource secondary;
    
    [SerializeField]
    AudioClip defaultMusic;
    [SerializeField]
    AudioClip redMusic;
    [SerializeField]
    AudioClip greenMusic;
    [SerializeField]
    AudioClip purpleMusic;
    [SerializeField]
    AudioClip yellowMusic;
    [SerializeField]
    AudioClip blueMusic;
    
    private static Dictionary<ItemType, AudioClip> clips;
    
    public bool shouldPlay = true;
    
    
    void Awake()
    {
        instance = this;
        
        clips = new Dictionary<ItemType, AudioClip>();
        clips.Add(ItemType.None, defaultMusic);
        clips.Add(ItemType.Red, redMusic);
        clips.Add(ItemType.Green, greenMusic);
        clips.Add(ItemType.Blue, blueMusic);
        clips.Add(ItemType.Purple, purpleMusic);
        clips.Add(ItemType.Yellow, yellowMusic);
    }
    
    void Start()
    {
        source1.clip = defaultMusic;
        source1.Play();
        primary = source1;
        secondary = source2;
        secondary.volume = 0;
    }

	public static void ChangeMusic(ItemType item)
    {
        instance.ChangeMusic(clips[item]);
    }
    
    private void ChangeMusic(AudioClip music)
    {
        secondary.clip = music;
        secondary.Play();
        secondary.timeSamples = primary.timeSamples;
        
        //switch primary and secondary
        var buf = primary;
        primary = secondary;
        secondary = buf;
    }
    
    void Update()
    {
        if(shouldPlay)
        {
            if(primary.volume < 1)
            {
                primary.volume = Mathf.Min(primary.volume + Time.deltaTime, 1);
                secondary.volume = 1-primary.volume;
                if(secondary.volume == 0)
                {
                    secondary.Stop();
                }
            }
        }
        else
        {
            primary.volume = Mathf.Max(primary.volume - Time.deltaTime, 0);
            secondary.volume = Mathf.Max(secondary.volume - Time.deltaTime, 0);
        }
    }
    
    public static void Finish()
    {
        instance.shouldPlay = false;
    }
}
