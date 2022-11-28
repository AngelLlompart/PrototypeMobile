using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 10;
    [SerializeField] private TextMeshProUGUI turboValue;
    [SerializeField] private Slider turboBar;
    private Rigidbody _playerRb;
    private GameManager _gameManager;
    private float _ogTurbo = 50;
    private float turbo;
    private Touch theTouch;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerRb = gameObject.GetComponent<Rigidbody>();
        UpdateTurbo();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.position.x >= 126 && theTouch.position.x <= 326 && theTouch.position.y >= 450 && theTouch.position.y <= 650)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            } else if (theTouch.position.x >= 126 && theTouch.position.x <= 326 && theTouch.position.y >= 150 && theTouch.position.y <= 350)
            {
                transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
            }
            /*else if (theTouch.phase == TouchPhase.Moved)
            {
                Vector3 move = new Vector3();
                Vector3 rotate = new Vector3();
                touchEndPosition = theTouch.position;

                move = y > 0 ? Vector3.forward : Vector3.back;
                rotate = x > 0 ? new Vector3(0, 90, 0) : new Vector3(0, -90, 0);
            
                transform.Translate(move * Time.deltaTime * moveSpeed);
                transform.Rotate(rotate * Time.deltaTime);
            }*/
        }

        CarMovement();
        CarRotation();
        CarRestoreRotation();
        Turbo();

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 1, transform.position.z);
            transform.rotation = Quaternion.identity;
        }
    }

    private void CarMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
    }

    private void CarRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("HorizontalR") < 0)
        {
            if (_gameManager.invAxis)
            {
                transform.Rotate(0,90 * Time.deltaTime,0);
            }
            else
            {
                transform.Rotate(0, -90 * Time.deltaTime, 0);
            }
        } else if (Input.GetKey(KeyCode.D) || Input.GetAxis("HorizontalR") > 0)
        {
            if (_gameManager.invAxis)
            {
                transform.Rotate(0, -90 * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0,90 * Time.deltaTime,0);
            }
        }
    }

    private void Turbo()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetButton("FireRB") || Input.GetButton("FireLB")) && (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0) && turbo > 0)
        {
            turbo -= (10 * Time.deltaTime);
            UpdateTurbo();
            moveSpeed = 20;
        }
        else {
            moveSpeed = 10;
        }
    }

    public void AddTurbo()
    {
        if (turbo + 10 < 100)
        {
            turbo += 10;
        }
        else
        {
            turbo = 100;
        }
        
        UpdateTurbo();
    }

    private void UpdateTurbo()
    {
        turboBar.value = turbo;
        turboValue.text = (int) turbo + "%";
    }

    public void ResetTurbo()
    {
        turbo = _ogTurbo;
        UpdateTurbo();
    }

    public void SetTurbo(int ogTurbo)
    {
        _ogTurbo = ogTurbo;
    }
    
    private void CarRestoreRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerRb.velocity = Vector3.zero;
            _playerRb.angularVelocity = Vector3.zero; 
            Vector3 eulers = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulers.y, 0);
            
        }
    }
}
