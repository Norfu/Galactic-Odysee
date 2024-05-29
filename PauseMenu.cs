using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{
    public string leveltoload;
    public GameObject settings;
    public void StartGamePause()
    {
        
    }
    public void recommencer()
    {
        
    }
    public void Settings()
    {
        settings.SetActive(true);
    }
    public void Retour()
    {
        SceneManager.LoadScene(leveltoload);
    }
    public void QuitPause()
    {
        Application.Quit();
    }
    public void closesettingPause()
    {
        settings.SetActive(false);
    }
}
