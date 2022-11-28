using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    private GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        mainCamera.GetComponent<CameraFollow>().offset = new Vector3(0, 5, -7);
        Vector3 eulers = mainCamera.transform.eulerAngles;
        mainCamera.transform.rotation = Quaternion.Euler(eulers.x, 0, eulers.z);
    }
}
