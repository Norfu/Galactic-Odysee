using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RetourMenu : MonoBehaviour
{
    public string Menu;
    public void ToMenu()
    {
        SceneManager.LoadScene(Menu);
    }
}