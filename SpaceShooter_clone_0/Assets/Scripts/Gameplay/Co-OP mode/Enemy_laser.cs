using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Enemy_laser : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        moveDown();
        destroyLaser();
    }
    void moveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    [ServerRpc]
    void destroyLaser()
    {
        if (transform.position.y < -11f)
        {
            Destroy(gameObject);
        }
    }
}
