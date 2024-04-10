using Unity.Netcode;
using UnityEngine;

public class NetworkServer : MonoBehaviour

{
    private SpawnManagerCO_OP _spawnManagerCO_OP;
    private void Awake()
    {
      _spawnManagerCO_OP=GameObject.Find("PoolManager").GetComponent<SpawnManagerCO_OP>();
        gameObject.SetActive(true);
    }
    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        _spawnManagerCO_OP.StartSpawnning();
        gameObject.SetActive(false);

      

    }
    public void JoinButton()
    {
        NetworkManager.Singleton.StartClient();
        _spawnManagerCO_OP.StartSpawnning();
        gameObject.SetActive(false);
       
    }
 
}
