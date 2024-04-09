using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerLaser : NetworkBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
       
        MoveUp();
        DestroyLaserServerRpc();
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
    [ServerRpc]
    void DestroyLaserServerRpc()
    {
        
        if (transform.position.y > 11f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
     
}
