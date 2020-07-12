using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelSelect : MonoBehaviour
{
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI score3;

    private void Update()
    {
        score1.text = "Best : " + FormatTime(PlayerPrefs.GetFloat("HighscoreLevel 1"));
        score2.text = "Best : " + FormatTime(PlayerPrefs.GetFloat("HighscoreLevel 2"));
        score3.text = "Best : " + FormatTime(PlayerPrefs.GetFloat("HighscoreLevel 3"));
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void resetScore(int level)
    {
        if (level == 0)
        {
            resetScore(1);
            resetScore(2);
            resetScore(3);
        }
        else
        {
            PlayerPrefs.SetFloat("HighscoreLevel " + level, 3599.999f);
        }

    }
}
