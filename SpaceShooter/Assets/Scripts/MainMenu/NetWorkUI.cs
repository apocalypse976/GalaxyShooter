using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetWorkUI : MonoBehaviour
{
    [SerializeField] private Button _join;
    [SerializeField] private Button _host;

    private void Start()
    {
        _host.onClick.AddListener(() => {
          NetworkManager.Singleton.StartHost();
            SceneManager.LoadScene(2);
        });
        _join.onClick.AddListener( () =>
        {
            NetworkManager.Singleton.StartClient();
            SceneManager.LoadScene(2);
        });
    }

}


