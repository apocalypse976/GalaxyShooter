using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _explodeAnimPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _explosionClip;
    private Collider2D _col;
    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        try {
            _col = GetComponent<Collider2D>();
            _spawnManager=GameObject.Find("SpawnManager"). GetComponent<SpawnManager>();
        }
        catch {
            Debug.Log("Collider not found");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(0,0, _rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {

            Instantiate(_explodeAnimPrefab, transform.position, Quaternion.identity);
            AudioManager.instance.PlayAudio(_explosionClip);
            _col.enabled = false;
            Destroy(collision.gameObject);
            _spawnManager.startSpawning();
            Destroy(gameObject,0.25f);
           
        }
    }
}
