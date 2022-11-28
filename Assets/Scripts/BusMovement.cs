using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    private bool left;
    private float movespeed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (left)
        {
            transform.Translate(0,0,  movespeed * Time.deltaTime);
        }

        if (!left)
        {
            transform.Translate(0,0,  -movespeed * Time.deltaTime);
        }
        
        if (transform.position.z >= 111.5)
        {
            left = false;
        }

        if (transform.position.z <= 101.5)
        {
            left = true;
        }
    }
}
