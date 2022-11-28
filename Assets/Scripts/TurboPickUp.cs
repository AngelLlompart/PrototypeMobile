using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboPickUp : MonoBehaviour
{
    private AudioSource _turboAudio;
    private PlayerMovement _player;
    // Start is called before the first frame update
    void Start()
    {
        _turboAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _turboAudio.Play();
            _player.AddTurbo();
            StartCoroutine(DestroyLate());
        }
    }
    
    IEnumerator DestroyLate()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
    }
}
