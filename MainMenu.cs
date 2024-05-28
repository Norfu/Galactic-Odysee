using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string leveltoload;
    public string level;
    public GameObject settings;
    public void StartGame()
    {
        SceneManager.LoadScene(leveltoload);
    }
    public void Levels()
    {
        SceneManager.LoadScene(level);
    }
    public void Settings()
    {
        settings.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void closesetting()
    {
        settings.SetActive(false);
    }
}
