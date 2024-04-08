using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameover;
    private Player _player;
    private bool _onConsole;
    public bool IsCoOpMode;
    [SerializeField] private GameObject _firebutton, _joystick;


    // Start is called before the first frame update
    void Start()
    {
        
      
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _onConsole = true;
            _firebutton.gameObject.SetActive(false);
            _joystick.gameObject.SetActive(false);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _onConsole = false;
            _firebutton.gameObject.SetActive(true);
            _joystick.gameObject.SetActive(true);

        }
       
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex==2)
        {
            IsCoOpMode = true;
        }
        else if(scene.buildIndex==1)
        {
            IsCoOpMode = false;
        }
        Invoke("playerfind", 2f);
    }

    // Update is called once per frame
    void Update()
    {
         
        
        try
        {
            if (_player == null)
            {
                _isGameover = true;
            }
            if (_onConsole)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
                {
                    _player.laser();
                }
            }
        }
        catch
        {
            if(!_isGameover)
            {
                Debug.LogError("Player not found.");
            }
           
        }
        
    }
    public void restartButton()
    {
        if(_isGameover&& IsCoOpMode)
        {
            SceneManager.LoadScene(2);
        }
        else if (_isGameover && !IsCoOpMode)
        {
            SceneManager.LoadScene(1);
        }
    }
    void playerfind()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
}
