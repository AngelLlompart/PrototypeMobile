using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI txtTime;
    private int _time = 120;
    public float timer;
    private int _ogTimer;
    private int timeFrequenccy = 1;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= _ogTimer - timeFrequenccy)
        {
            _ogTimer -= timeFrequenccy;
            txtTime.text = "Time: " + _ogTimer;
        }

        if (timer <= 0)
        {
            _gameManager.EndTime();
        }
    }

    public void ResetTime()
    {
        timer = _time;
        _ogTimer = _time;
        txtTime.text = "Time: " + _ogTimer;
    }

    public void SetTime(int time)
    {
        _time = time;
    }
}
