using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public string pauseMenuSceneName = "Menu PAUSE";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Recommencer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        // Unload the pause menu scene
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        // Resume time
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        // Load the pause menu scene additively
        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
        // Pause time
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
