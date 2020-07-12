using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }


    

    
}
