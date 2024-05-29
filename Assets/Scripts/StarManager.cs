using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance { get; private set; }
    private int starsCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); starsCollected = PlayerPrefs.GetInt("StarsCollected", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectStar()
    {
        starsCollected++;
        PlayerPrefs.SetInt("StarsCollected", starsCollected);
    }

    public int GetStarsCollected()
    {
        return starsCollected;
    }
}
