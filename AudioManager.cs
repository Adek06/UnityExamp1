﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;
    public static AudioManager Instance {
        get { return _instance; }
    }

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public AudioSource efxSource;

    public void RandomPlay(params AudioClip[] clips) {
        float pitch = Random.Range(minPitch, maxPitch);
        int index = Random.Range(0,clips.Length);
        AudioClip clip = clips[index];
        efxSource.clip = clip;
        efxSource.pitch = pitch;
        efxSource.Play();
    }
	// Use this for initialization
	void Awake () {
        _instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
