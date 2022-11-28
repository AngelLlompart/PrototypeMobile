using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    private bool finalCoroutine = false;
    private bool left = false;
    void Start()
    {
        StartCoroutine(Circuito());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 4.2f * Time.deltaTime);
        if(left)transform.Rotate(0, -30 * Time.deltaTime, 0);
        //transform.Rotate(0, -20 * Time.deltaTime, 0);
        if (finalCoroutine)
        {
            StartCoroutine(Circuito());
        }
    }

    IEnumerator Circuito()
    {
        finalCoroutine = false;
        yield return new WaitForSecondsRealtime(6);
        left = true;
        yield return new WaitForSecondsRealtime(6);
        left = false;
        yield return new WaitForSecondsRealtime(6);
        left = true;
        yield return new WaitForSecondsRealtime(6);
        left = false;
        finalCoroutine = true;
    }
}
