using System.Collections;
using Unity.Netcode;
using UnityEngine;
using Unity.BossRoom.Infrastructure;


public class SpawnManagerCO_OP : NetworkBehaviour
{
    [SerializeField] private GameObject _enemyprefab;
    [SerializeField] private GameObject[] _powerUps;
    private const int MaxprefabCount = 30;


    private void Start()
    { 
        NetworkManager.Singleton.OnClientStarted += StartSpawnning;
    }
    public void StartSpawnning()
    {
        NetworkManager.Singleton.OnClientStarted -= StartSpawnning;
        NetworkObjectPool.Singleton.InitializePool();
        StartCoroutine(StartEnemySpawnRoutine());
        StartCoroutine(StartPowerUpsSpawnRoutine());
        
    }
    void SpawnEnemy()
    {
        Vector3 Pos = new Vector3(Random.Range(-7, 7), 7.5f, 0);
        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(_enemyprefab, Pos,Quaternion.identity);
       obj. GetComponent<enemy>().Prefab= _enemyprefab;
        if(!obj.IsSpawned)
        obj.Spawn(true);
    }
    void SpawnPowerUps()
    {
        Vector3 Pos = new Vector3(Random.Range(-7, 7), 7.5f, 0);
        int powerUpsId = Random.Range(0,_powerUps.Length);
        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(_powerUps[powerUpsId], Pos, Quaternion.identity);
        if (!obj.IsSpawned)
            obj.Spawn(true);

    }
  IEnumerator StartEnemySpawnRoutine()
  {
        while (NetworkManager.Singleton.ConnectedClients.Count> 0)
        {
            yield return new WaitForSeconds(2);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabs(_enemyprefab) < MaxprefabCount)
            {
                if (NetworkManager.Singleton.ConnectedClients.Count > 1)
                {
                    SpawnEnemy();
                }

            }

        }
  }
    IEnumerator StartPowerUpsSpawnRoutine()
    {
        while(NetworkManager.Singleton.ConnectedClients.Count>0)
        {
            yield return new WaitForSeconds(Random.Range(5, 15));
            if (NetworkManager.Singleton.ConnectedClients.Count > 1)
            {
                SpawnPowerUps();
            }
        
        }
    }
    
}
