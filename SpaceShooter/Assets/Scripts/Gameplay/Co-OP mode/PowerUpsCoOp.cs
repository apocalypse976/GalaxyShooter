using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PowerUpsCoOp : NetworkBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        Move();
        destroyGameObjServerRpc();
    }
    private void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    [ServerRpc]
    void destroyGameObjServerRpc()
    {
        if (transform.position.y < -11f)
        {
            Destroy(gameObject);
        }

     }
}
