﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public AudioClip[] se;
    public AudioSource audioSE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound2D(int id)
    {
        audioSE.PlayOneShot(se[id]);
    }
}