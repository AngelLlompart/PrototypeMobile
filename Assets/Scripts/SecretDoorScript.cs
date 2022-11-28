using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorScript : MonoBehaviour
{

    [SerializeField] private GameObject secretWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            secretWall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            secretWall.SetActive(true);
        }
    }
}
