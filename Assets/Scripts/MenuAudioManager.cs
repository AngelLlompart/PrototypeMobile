using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    private MainMenuManager _mainMenuManager;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _mainMenuManager = FindObjectOfType<MainMenuManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void Update()
    {
        _audioSource.volume = _mainMenuManager.musicVolume;
    }
}
