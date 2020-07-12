using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [Header("Player States")]
    public PlayerState playerState;
    public CarController carController;
    public TextMeshProUGUI SpeedUI;

    [Header("Time Control")]
    public TextMeshProUGUI countdownTimer;
    public LapTimer laptimer;
    private float maxTimer = 5f;
    private float timer;
    private float timerSound;
    private bool timerRunning = true;
    [SerializeField] bool useCountdown = true;
    public AudioSource countdownBeep;
    public AudioSource goBeep;

    [Header("Level")]
    public TextMeshProUGUI levelNameText;
    public SpriteRenderer startLightRenderer;
    public GameObject[] AICars;

    // Start is called before the first frame update
    void Start()
    {
        levelNameText.text = laptimer.getLevelName();
        timer = maxTimer;
        timerSound = (int)maxTimer;
        AICars = GameObject.FindGameObjectsWithTag("AICar");
        
    }


    // Update is called once per frame
    void Update()
    {
        countdown();
        updateSpeedUI();
    }
    
    void countdown()
    {
        if (!useCountdown)
        {
            StartCoroutine(hideCountdownTimer(0f));
            carController.isRunning = true;
            laptimer.isRunning = true;
            setAllAICarRunning(true);
        }
        else if (timerRunning)
        {
            countdownTimer.enabled = true;
            if (timer >= 0)
            {
                timer -= 1 * Time.deltaTime;
                countdownTimer.text = timer.ToString("0");
                startLightRenderer.material.SetFloat("_ShiftValue", 1-(timer) / maxTimer);
            }
            if (timer < 0.5f)
            {
                goBeep.Play();
                countdownTimer.text = "GO!";
                carController.isRunning = true;
                laptimer.isRunning = true;
                setAllAICarRunning(true);

                laptimer.ResetCurrentTime();
                StartCoroutine(hideCountdownTimer(1.5f));
                timerRunning = false;
            }

            if (countdownTimer.text.Equals(timerSound.ToString("0")))
            {
                countdownBeep.Play();
                //print(timerSound.ToString("0") + " : " + countdownTimer.text);
                timerSound--;
            }
           
        }
    }

    IEnumerator hideCountdownTimer(float timeout)
    {
        yield return new WaitForSeconds(timeout);
        countdownTimer.enabled = false;

    }

    void updateSpeedUI()
    {
        SpeedUI.text = playerState.getSpeed().ToString("0");
    }

    void setAllAICarRunning(bool b)
    {
        foreach(GameObject AICar in AICars)
        {
            AICar.GetComponent<CarController>().isRunning = b;
        }
    }

   
}
