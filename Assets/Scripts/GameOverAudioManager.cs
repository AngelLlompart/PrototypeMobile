using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAudioManager : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    private void Update()
    {
        _audioSource.volume = _gameManager.musicVolume;
    }
}
