using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private GameObject bullet;
    private AudioSource _shootAudioSource;
    private GameObject player;
    private bool finalCoroutine = false;

    private float bulletSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        _shootAudioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (finalCoroutine)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        finalCoroutine = false;
        _shootAudioSource.Play();
        GameObject newBullet = Instantiate(bullet, bulletSpawner.transform.position, transform.rotation);
        Vector3 eulers = newBullet.transform.eulerAngles;
        newBullet.transform.rotation = Quaternion.Euler(90,eulers.y, eulers.z);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.up * bulletSpeed;
        yield return new WaitForSecondsRealtime(2);
        finalCoroutine = true;
    }
}
