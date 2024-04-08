using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public  void SinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
    public void Co_Op()
    {
        SceneManager.LoadScene(3);
    }
    public void Settings()
    {

    }
    public void quit()
    {
        Application.Quit();
    }

}
