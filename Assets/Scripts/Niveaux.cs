using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveaux : MonoBehaviour
{
    public string leveltoload;
    public string leveltoload1;
    public string leveltoload2;
    public string leveltoload3;
    public string retour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Startlevel()
    {
        SceneManager.LoadScene(leveltoload);
    }

    public void Levels1()
    {
        SceneManager.LoadScene(leveltoload1);
    }

    public void Levels2()
    {
        SceneManager.LoadScene(leveltoload2);
    }

    public void Levels3()
    {
        SceneManager.LoadScene(leveltoload3);
    }

    public void Retour()
    {
        SceneManager.LoadScene(retour);
    }

}
