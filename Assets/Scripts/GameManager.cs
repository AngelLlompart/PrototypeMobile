using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI txtWin;
    
    [SerializeField] private TextMeshProUGUI hpValue;
    [SerializeField] private TextMeshProUGUI txtPoints;
    [SerializeField] private Button btnOk;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnSave;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnYes;
    [SerializeField] private Button btnNo;
    [SerializeField] private GameObject secureBox;
    private GameObject _player;
    private GameObject _mainCamera;
    private Timer _timer;
    private bool _pause = false;
    private int hp = 100;
    private int points;
    private int ogHp;
    private int points2;
    private bool _save = false;
    private bool _quit = false;
    public bool invAxis = false;
    public int level;
    private int initLevel;
    private bool finished = false;
    public float musicVolume;
    private bool win = false;

    private AudioSource _gameManagerAudioSource;

    [SerializeField] private AudioClip pauseAudioClip;

    [SerializeField] private AudioClip unpauseAudioClip;
    
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        musicVolume = 0.5f;
        _gameManagerAudioSource = gameObject.GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level",1);
            PlayerPrefs.Save();
        }
        level = PlayerPrefs.GetInt("level");
        Debug.Log(level);
        initLevel = PlayerPrefs.GetInt("level");
        btnOk.onClick.AddListener(GameOver);
        btnResume.onClick.AddListener(ResumeGame);
        btnSave.onClick.AddListener(SaveGame);
        btnRestart.onClick.AddListener(RestartLevel);
        btnMenu.onClick.AddListener(MainMenu);
        btnQuit.onClick.AddListener(Quit);
        btnYes.onClick.AddListener(ComfirmExit);
        btnNo.onClick.AddListener(CancelExit);
        InitLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) && _pause == false && finished == false)
        {
            PauseGame();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")) && _pause)
        {
            ResumeGame();
        }

        if (finished && Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
        }
    }

    public void Damage(int dmgAmount)
    {
        hp -= dmgAmount;
        ShowLife();
        if (hp <= 0)
        {
            //Destroy(_player);
            //Destroy(FindObjectOfType<CameraFollow>());
            _player.SetActive(false);
            win = false;
            EndLevel("You died!");
        }
    }

    private void ShowLife()
    {
        healthBar.value = hp;
        if (hp <= 0)
        {
            hpValue.text = 0 + "%";  
        }
        else
        {
            hpValue.text = hp + "%";  
        }
        if (hp < 50)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        }
    }

    private void PauseGame()
    {
        _gameManagerAudioSource.PlayOneShot(pauseAudioClip, 2.0f);
        _pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        pauseMenu.gameObject.SetActive(true);
    }
    
    private void ResumeGame()
    {
        _gameManagerAudioSource.PlayOneShot(unpauseAudioClip, 2.0f);
        Debug.Log("aaa");
        _pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("level", level);
        if (level == 2)
        {
            PlayerPrefs.SetInt("hp", ogHp);
            PlayerPrefs.SetInt("points",points2);
        }
        PlayerPrefs.Save();
        _save = true;
    }

    private void RestartLevel()
    {
        InitLevel();
    }

    private void MainMenu()
    {
        if (_save == false)
        {
            secureBox.SetActive(true);
            _quit = false;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
    private void Quit()
    {
        if (_save == false)
        {
            secureBox.SetActive(true);
            _quit = true;
        }
        else
        {
            #if UNITY_EDITOR
                if(EditorApplication.isPlaying) 
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            #else
                Application.Quit();
            #endif
        }
    }

    private void ComfirmExit()
    {
        level = 1;
        PlayerPrefs.SetInt("level", 1);
        if (_quit)
        {
        #if UNITY_EDITOR
            if(EditorApplication.isPlaying) 
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        #else
            Application.Quit();
        #endif
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        secureBox.SetActive(false);
    }

    private void CancelExit()
    {
        secureBox.SetActive(false);
    }
    
    public void EndTime()
    {
        win = false;
        EndLevel("Time has ended!");
    }

    
    public void ArriveGoal()
    {
        win = true;
        //Debug.Log(hp/100f);
        
        //total of 800 points, 400 depending on hp%, and 400 depending on time
        //if time is less than 100, for every 10 seconds 40 points are rested, when time is less than 10, 0 points are gotten
        points += (int) (400 * (hp / 100f)) + 400;
        if (_timer.timer < 100)
        {
            //Debug.Log(40 * Math.Ceiling((100 - _timer)/10f));
            points -= (int) (40 * Math.Ceiling((100 - _timer.timer)/10f));
        }
        //Debug.Log(points);
        txtPoints.text = "Points: " + points;
        if(level == 1)
        {
            EndLevel("Congratulations, go to next level.");
        }
        else
        {
            EndLevel("YOU WIN!");
        }
    }
    private void EndLevel(String message)
    {
        finished = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        txtWin.text = message;
        txtWin.gameObject.SetActive(true);
        btnOk.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void GameOver()
    {
        if (level == 1 && win)
        {
            win = false;
            level = 2;
            SceneManager.LoadScene("Level2");
            ogHp = hp;
            points2 = points;
        } else if (level == 2 && !win)
        {
            _player.SetActive(true);
            SceneManager.LoadScene("GameOver");
            //hp = ogHp;
            //points = points2;
        }
        else
        {
            level = 1;
            _player.SetActive(true);
            SceneManager.LoadScene("GameOver");
        }
        InitLevel();
    }

    public void InitLevel()
    {
        //si guarda en el nivel 2, para que recoja los valores con los que ha acabado el nivel 1, al volver a abrir
        if (initLevel == 2)
        {
            ogHp = PlayerPrefs.GetInt("hp");
            points2 = PlayerPrefs.GetInt("points");
        }
        else
        {
            hp = 100;
        }
        _timer = FindObjectOfType<Timer>();
        _player = GameObject.FindWithTag("Player");
        _mainCamera = GameObject.FindWithTag("MainCamera");
        
        if (level == 1)
        {
            points2 = 0;
            ogHp = 100;
            _timer.SetTime(120);
            _player.GetComponent<PlayerMovement>().SetTurbo(50);
        }
        else
        {
            _timer.SetTime(80);
            _player.GetComponent<PlayerMovement>().SetTurbo(30);
        }

        Debug.Log("a" + level);
        points = points2;
        finished = false;
        _pause = false;
        _save = false;
        _quit = false;
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        txtWin.gameObject.SetActive(false);
        btnOk.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        secureBox.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        hp = ogHp;
        txtPoints.text = "Points: " + points;
        _mainCamera.GetComponent<CameraFollow>().offset = new Vector3(0, 5, -7);
        Vector3 eulers = _mainCamera.transform.eulerAngles;
        _mainCamera.transform.rotation = Quaternion.Euler(eulers.x, 0, eulers.z);
        _player.transform.position = new Vector3(0, 0.5f, -5.48f);
        _player.transform.rotation = Quaternion.Euler(0,0,0);
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
        
        ShowLife();
        _timer.ResetTime();
        _player.GetComponent<PlayerMovement>().ResetTurbo();
    }
}
