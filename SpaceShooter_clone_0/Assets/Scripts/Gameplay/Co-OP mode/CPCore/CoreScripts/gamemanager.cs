using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    [SerializeField] private GameObject _joyStick;
    [SerializeField] private GameObject _shootButton;
    [SerializeField] private GameObject _serverButton;
    private bool _isConsole;
    public static gamemanager Singleton;
    private CO_OP_player _player;

    private void Start()
    {
       _serverButton.gameObject.SetActive(true);
        _joyStick.gameObject.SetActive(false);
        _shootButton.gameObject.SetActive(false);
        if (Singleton = null)
        {
            Singleton = this;
        }
        else if(Singleton != null)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    { 
        if (_isConsole)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _player.SpawnLaserServerRpc();
            }
        }
       
    }
    #region Host & Join
    public void HostButton()
    {
        try
        {
            NetworkManager.Singleton.StartHost();
            _serverButton. gameObject.SetActive(false);
            _player = GameObject.FindWithTag("Player").GetComponent<CO_OP_player>();
            DeviceDetection();
        }
        catch(System.Exception e) 
        {
            Debug.LogError(e.Source);
        }
        

    }
    public void JoinButton()
    {
        try
        {
            NetworkManager.Singleton.StartClient();
           _serverButton. gameObject.SetActive(false);
            _player = GameObject.FindWithTag("Player").GetComponent<CO_OP_player>();
            DeviceDetection();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
       
    }
    #endregion

    #region Device Detect
    void DeviceDetection()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _joyStick.gameObject.SetActive(true);
            _shootButton.gameObject.SetActive(true);
            _isConsole = false;
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _joyStick.gameObject.SetActive(false);
            _shootButton.gameObject.SetActive(false);
            _isConsole = true;
           
        }
    }
    #endregion
    
}
