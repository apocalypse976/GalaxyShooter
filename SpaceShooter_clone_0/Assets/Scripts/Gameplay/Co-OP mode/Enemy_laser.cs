using Unity.Netcode;
using UnityEngine;

public class Enemy_laser : NetworkBehaviour
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
    void destroyLaser()
    {
        if (transform.position.y < -11f)
        {
            NetworkObject.Despawn();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"||collision.tag=="PlayerLaser")
        {
            NetworkObject.Despawn();
        }
    }
}
