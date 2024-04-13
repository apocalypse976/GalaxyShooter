using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _settingsScreen;
    private void Start()
    {
        _settingsScreen.SetActive(false);
    }
    public  void SinglePlayer()
    {
        SceneManager.LoadScene(1);
        Time.timeScale= 1;
    }
    public void Co_Op()
    {
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {
        _settingsScreen.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        _settingsScreen.SetActive(false);
    }

}
