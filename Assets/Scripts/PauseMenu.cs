using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settings;

    public void StartGamePause()
    {
        // Implementation for starting game pause (if needed)
    }

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void Retour()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitPause()
    {
        Application.Quit();
    }

    public void CloseSettingPause()
    {
        settings.SetActive(false);
    }
}
