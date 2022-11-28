using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private GameManager _gameManager;

    private AudioSource _playerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _playerAudioSource = gameObject.GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _playerAudioSource.Play();
            _gameManager.Damage(10);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerAudioSource.Play();
            _gameManager.Damage(20);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            _playerAudioSource.Play();
            _gameManager.Damage(30);
        }
    }
}
