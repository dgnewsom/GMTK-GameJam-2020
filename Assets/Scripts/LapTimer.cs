using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using System;
using Unity.Mathematics;

public class LapTimer : MonoBehaviour
{

    [Header("Time Loader")]
    [SerializeField] int lapCounter = 0;
    [SerializeField] private float startTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int numberOfLaps = 5;
    public TextMeshProUGUI TimeUI; 
    public TextMeshProUGUI LapNumber;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LapTimes;
    public TextMeshProUGUI CurrentLap;

    public LevelComplete levelComplete;

    public bool isRunning = false;
    private bool isHighscore = false;
    public string LevelName = "Level";

    private float[] times;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("Highscore" + LevelName) == 0f)
        {
            PlayerPrefs.SetFloat("Highscore" + LevelName, 3599.999f);
        }
        times = new float[numberOfLaps+1];
        LapNumber.text = ("Lap " + (lapCounter+1));
        HighScore.text = "Best Time : " + FormatTime(PlayerPrefs.GetFloat("Highscore" + LevelName));
        LapTimes.text = "Lap " + (lapCounter + 1) + " : --:--:---";
        isRunning = false;
        ResetCurrentTime();

    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime = Time.time - startTime;
            UpdateTimeUI();
        }
        //test lap complete
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Test Lap");
            CompleteLap();
        }
        */
    }

    public void CompleteLap()
    {
        if (isRunning)
        {
            times[lapCounter] = currentTime;
            calculateTotalTime();
            LapTimes.text = LapTimes.text.Substring(0, LapTimes.text.Length - 9) + string.Format("{0}\nLap {1} : --:--:---", FormatTime(times[lapCounter]), lapCounter + 2);
            lapCounter += 1;
            LapNumber.text = "Lap " + (lapCounter + 1);
            ResetCurrentTime();

            //If is the last lap calculate and display total time check for highscore.
            if (lapCounter == numberOfLaps)
            {
                calculateTotalTime();
                LapTimes.text = LapTimes.text.Substring(0, LapTimes.text.Length - 17);
                CurrentLap.text = string.Format("Total Time : {0}", FormatTime(times[lapCounter]));
                TimeUI.text = string.Format("Total Time\n{0}", FormatTime(times[lapCounter]));
                LapNumber.text = "FINISHED!";
                CheckHighScore(times[numberOfLaps]);
                isRunning = false;
                levelComplete.DisplayCompleteScreen(GetScoreStringArray(), GetHighScoreString(),isHighscore);
            }
        }
        
    }

    void calculateTotalTime()
    {
        float totalTime = 0;
        for(int x =0; x<times.Length-1;x++)
        {
            totalTime += times[x];
        }
        times[numberOfLaps] = totalTime;
    }

    void CheckHighScore(float totalTime)
    {
        if (totalTime < PlayerPrefs.GetFloat("Highscore" + LevelName))
        {
            PlayerPrefs.SetFloat("Highscore" + LevelName, totalTime);
            isHighscore = true;
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    void UpdateTimeUI()
    {
        TimeUI.text = currentTime.ToString("0");
        CurrentLap.text = "Current Lap : " + FormatTime(currentTime);
    }

    public void ResetCurrentTime()
    {
        startTime = Time.time;
    }

    public string[] GetScoreStringArray()
    {
        string[] output = new string[times.Length];

        for (int x = 0; x < times.Length; x++) 
        {
            if (times[x] == 0f)
            {
                output[x] = "--:--:---";
            }
            else
            {
                output[x] = FormatTime(times[x]);
            }
            
        }

        return output;
    }

    public string GetHighScoreString()
    {
        return FormatTime(PlayerPrefs.GetFloat("Highscore" + LevelName));
    }

    public string getLevelName()
    {
        return LevelName;
    }

}
