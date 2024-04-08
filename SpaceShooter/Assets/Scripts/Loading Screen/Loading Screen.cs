using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;

    private void Start()
    {
       
        InternetConnection();
      
    }
    
    void InternetConnection()
    {
     if (Application.internetReachability != NetworkReachability.NotReachable)
      {
            _loadingText.text = "Cheking For Internet.......";
            SceneManager.LoadScene(4);
       }
       else
        {
            _loadingText.text = "Internet not available.......";
        }
        
    }



}
