using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class enemy : NetworkBehaviour
{
    [HideInInspector] public GameObject Prefab;
    [SerializeField] private float _speed=10f;
    [SerializeField] private NetworkObject _laserPrefab;
    private float _firerate = 2f;
    private float _canfire = 0;

    private void Update()
    {
        EnemyMove();
        if (!IsOwner) return;
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
            if (!IsOwner) return;
            destroyEnemyServerRpc();


        }
    }
    [ServerRpc]
    void destroyEnemyServerRpc()
    {
        NetworkObject.Despawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!IsOwner) return;
            destroyEnemyServerRpc();
        }
        if (collision.tag == "PlayerLaser")
        {
            if (!IsOwner) return;
            destroyEnemyServerRpc();
        }
    }

}
