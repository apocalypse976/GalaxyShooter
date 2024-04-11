using Unity.Netcode;
using UnityEngine;

public class NetworkServer : MonoBehaviour

{
    private void Awake()
    { 
        gameObject.SetActive(true);
    }
    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        gameObject.SetActive(false);
      

    }
    public void JoinButton()
    {
        NetworkManager.Singleton.StartClient();
        gameObject.SetActive(false);
    }
}
