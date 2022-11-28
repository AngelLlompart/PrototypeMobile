using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider volumeBar;
    [SerializeField] private TextMeshProUGUI volumeValue;
    [SerializeField] private Toggle axisToggle;
    [SerializeField] private Button btnCloseSettings;
    private GameManager _gameManager;

    public float musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        musicVolume = volumeBar.value;
        _gameManager = FindObjectOfType<GameManager>();
        settingsMenu.SetActive(false);
        btnPlay.onClick.AddListener(Play);
        btnSettings.onClick.AddListener(Settings);
        btnQuit.onClick.AddListener(Quit);
        btnCloseSettings.onClick.AddListener(CloseSettings);
       // _gameManager.PauseGame();
       Destroy(GameObject.Find("Level Audio Manager"));
       StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = volumeBar.value;
        _gameManager.musicVolume = volumeBar.value;
        volumeValue.text = (int) (volumeBar.value * 100) + "%";
        if (axisToggle.isOn)
        {
            _gameManager.invAxis = true;
        }
        else
        {
            _gameManager.invAxis = false;
        }
    }

    private void Play()
    {
        _gameManager.InitLevel();
        if (_gameManager.level == 1)
        { 
            SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.Log("level " + _gameManager.level);
            SceneManager.LoadScene("Level2");
        }
        
    }

    private void Settings()
    {
        Debug.Log("aaa");
        settingsMenu.SetActive(true);
    }

    private void Quit()
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

    private void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
    
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
