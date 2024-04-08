using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _player;
    private Enemy _enemy;
    

    private void Start()
    {
        try 
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        catch
        {
            Debug.LogError("Player not found");
        }
       
        
    }

    private void Update()
    {
        

        Movement();
       
    }
    void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -11f)
        {
            Destroy(gameObject);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player.Damage();

        }
        if (collision.tag == "Laser")
        {
            Destroy(gameObject);
        }
    }
}
