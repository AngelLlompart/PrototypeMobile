using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    private GameObject _levelAudioManagerGameObject;
    public static LevelAudioManager instance;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    
    private void Awake()
    {
        _levelAudioManagerGameObject = GameObject.Find("Level Audio Manager");
        _gameManager = FindObjectOfType<GameManager>();
        if(instance != null && instance != this){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(_levelAudioManagerGameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void Update()
    {
        _audioSource.volume = _gameManager.musicVolume;
    }
}
