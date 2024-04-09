using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class enemy : NetworkBehaviour
{
    [SerializeField] private float _speed=10f;
    [SerializeField] private NetworkObject _laserPrefab;
    private float _firerate = 2f;
    private float _canfire = 0;

    private void Update()
    {
        EnemyMove();
        EnemyFireServerRpc();
        DestroyEnemy();
    }

    void EnemyMove()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime) ;
    }
    [ServerRpc]
    void EnemyFireServerRpc()
    {
        if (Time.time > _canfire)
        {
            _canfire = _firerate + Time.time;
          StartCoroutine(spawnLaserRoutine());
            
        }
    }
    IEnumerator spawnLaserRoutine()
    {
        while (true){
            Vector3 SpawnPos= new Vector3(transform.position.x,transform.position.y-1.5f,transform.position.z);
           NetworkObject laserInstance = Instantiate(_laserPrefab, SpawnPos, Quaternion.identity);
            laserInstance.SpawnWithOwnership(OwnerClientId);
            yield return new WaitForSeconds(3);
        }
     
    }
    void DestroyEnemy()
    {
        if (transform.position.y < -6.5f)
        {
            Destroy(gameObject);
        }
    }

}
