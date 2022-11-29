using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private int moveSpeed = 10;
    private int turboSpeed = 20;
    [SerializeField] private TextMeshProUGUI turboValue;
    [SerializeField] private Slider turboBar;
    private Rigidbody _playerRb;
    private GameManager _gameManager;
    private float _ogTurbo = 50;
    private float turbo;
    private Touch theTouch;
    [SerializeField] private GameObject btnUp;
    [SerializeField] private GameObject btnDown;
    [SerializeField] private GameObject btnLeft;
    [SerializeField] private GameObject btnRight;
    [SerializeField] private GameObject btnTurbo;
    [SerializeField] private GameObject btnReset;
    private Vector3 btnUpPosition;
    private Vector3 btnDownPosition;
    private Vector3 btnLeftPosition;
    private Vector3 btnRightPosition;
    private Vector3 btnTurboPosition;
    private Vector3 btnResetPosition;
    private float btnMovementsSize;
    private float btnTurboSize;
    private float btnResetHeight;
    private float btnResetWidth;

    private Touch touch2;
    // Start is called before the first frame update
    void Start()
    {
        btnUpPosition = btnUp.transform.position;
        btnDownPosition = btnDown.transform.position;
        btnLeftPosition = btnLeft.transform.position;
        btnRightPosition = btnRight.transform.position;
        btnTurboPosition = btnTurbo.transform.position;
        btnResetPosition = btnReset.transform.position;
        Debug.Log(btnUpPosition);
        btnResetHeight = btnReset.GetComponent<RectTransform>().rect.height / 2 * btnReset.transform.lossyScale.y;
        btnResetWidth = btnReset.GetComponent<RectTransform>().rect.width / 2 * btnReset.transform.lossyScale.x;
        btnMovementsSize = btnUp.GetComponent<RectTransform>().rect.height / 2 * btnUp.transform.lossyScale.y;
        btnTurboSize = btnTurbo.GetComponent<RectTransform>().rect.height / 2 * btnTurbo.transform.lossyScale.y;
        Debug.Log(btnUp.transform.lossyScale);
        Debug.Log(btnDownPosition);
        Debug.Log(btnLeftPosition);
        Debug.Log(btnRightPosition);
        _gameManager = FindObjectOfType<GameManager>();
        _playerRb = gameObject.GetComponent<Rigidbody>();
        UpdateTurbo();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                theTouch = Input.GetTouch(i);
                // touch2 = Input.GetTouch(1);
                if ((theTouch.position.x >= btnTurboPosition.x - btnTurboSize && theTouch.position.x <= btnTurboPosition.x + btnTurboSize &&
                    theTouch.position.y >= btnTurboPosition.y - btnTurboSize && theTouch.position.y <= btnTurboPosition.y + btnTurboSize) && turbo > 0)
                {
                    turbo -= (10 * Time.deltaTime);
                    UpdateTurbo();
                    transform.Translate(Vector3.forward * Time.deltaTime * turboSpeed);
                }

                if (theTouch.position.x >= btnUpPosition.x - btnMovementsSize && theTouch.position.x <= btnUpPosition.x + btnMovementsSize &&
                    theTouch.position.y >= btnUpPosition.y - btnMovementsSize && theTouch.position.y <= btnUpPosition.y + btnMovementsSize)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                }
                else if (theTouch.position.x >= btnDownPosition.x - btnMovementsSize &&
                         theTouch.position.x <= btnDownPosition.x + btnMovementsSize &&
                         theTouch.position.y >= btnDownPosition.y - btnMovementsSize &&
                         theTouch.position.y <= btnDownPosition.y + btnMovementsSize)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                }

                if (theTouch.position.x >= btnLeftPosition.x - btnMovementsSize && theTouch.position.x <= btnLeftPosition.x + btnMovementsSize &&
                    theTouch.position.y >= btnLeftPosition.y - btnMovementsSize && theTouch.position.y <= btnLeftPosition.y + btnMovementsSize)
                {
                    transform.Rotate(0, -90 * Time.deltaTime, 0);
                }
                else if (theTouch.position.x >= btnRightPosition.x - btnMovementsSize &&
                         theTouch.position.x <= btnRightPosition.x + btnMovementsSize &&
                         theTouch.position.y >= btnRightPosition.y - btnMovementsSize &&
                         theTouch.position.y <= btnRightPosition.y + btnMovementsSize)
                {
                    Debug.Log("NOP");
                    transform.Rotate(0, 90 * Time.deltaTime, 0);
                }

                if (theTouch.position.x >= btnResetPosition.x - btnResetWidth && theTouch.position.x <= btnResetPosition.x + btnResetWidth &&
                    theTouch.position.y >= btnResetPosition.y - btnResetHeight && theTouch.position.y <= btnResetPosition.y + btnResetHeight && theTouch.phase == TouchPhase.Ended)
                {
                    Debug.Log("yes");
                    CarRestoreRotation();
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
        }

        CarMovement();
        CarRotation();

        if (Input.GetKeyDown(KeyCode.R))
        {
            CarRestoreRotation();
        }

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
        _playerRb.velocity = Vector3.zero;
        _playerRb.angularVelocity = Vector3.zero; 
        Vector3 eulers = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulers.y, 0);
    }
}
