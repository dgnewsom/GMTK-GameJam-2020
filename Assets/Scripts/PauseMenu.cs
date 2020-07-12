using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public Camera camera;
    public LapTimer lapTimer;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI timesText;
    public TextMeshProUGUI bestTimeText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf)
        {
            
            string[] times = lapTimer.GetScoreStringArray();

            string timesString = "";
            for (int x = 0; x < (times.Length - 1); x++)
            {
                timesString += string.Format("Lap {0} : {1}\n", x + 1, times[x]);
            }

            totalTimeText.text = "Total Time : " + times[times.Length - 1];
            timesText.text = timesString;
            bestTimeText.text = "Best Time : " + lapTimer.GetHighScoreString();
            camera.GetComponent<AudioListener>().enabled = false;
            Time.timeScale = 0f;
        }
        else
        {
            camera.GetComponent<AudioListener>().enabled = true;
            Time.timeScale = 1f;
        }
    }

}
