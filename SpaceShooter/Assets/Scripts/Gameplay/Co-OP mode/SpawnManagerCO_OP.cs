using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class SpawnManagerCO_OP : NetworkBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private NetworkObject _enemyPrefab;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) return;
        StartRoutinServerRpc();
    }
    private void Update()
    {
       
    }
    [ServerRpc]
    public void StartRoutinServerRpc()
    {
        
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
       
        while (true)
        {
            int spawnPos = Random.Range(-7,7);
           NetworkObject _enemyPrefabInstance= Instantiate(_enemyPrefab, new Vector3(spawnPos, 7, 0), Quaternion.identity);
            _enemyPrefabInstance.SpawnWithOwnership(OwnerClientId);
            yield return new WaitForSeconds(Random.Range(2,5));
        }
    }
}
