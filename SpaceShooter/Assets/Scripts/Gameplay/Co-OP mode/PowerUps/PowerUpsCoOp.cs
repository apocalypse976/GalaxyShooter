using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpsCoOp : NetworkBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        Move();
        if (!IsOwner) return;
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
    [ServerRpc]
    void DestroyServerRpc()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(!IsOwner) return;
            DestroyServerRpc();
        }
    }
}
