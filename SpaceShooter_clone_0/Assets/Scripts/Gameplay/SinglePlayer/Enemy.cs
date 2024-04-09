using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _MinX, _MaxX;
    [SerializeField] private AudioClip _destroyClip;
    [SerializeField] private GameObject _laserPrefab;
    private float _canFire=0, _firerate=2f;
    private Collider2D _boxCollider;
    private Animator _anim;
    private Player _player;
    private float _randX;
    

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _anim = GetComponent<Animator>();
            _boxCollider= GetComponent<Collider2D>();
        }
        catch
        {
            UnityEngine.Debug.LogError("Didnot found Player");
        }
        
        transform.position= new Vector3(Random.Range(-8.5f,8.8f),8,0);
    }

    void Update()
    {
        Movement();
       Firelaser();

    }

    void Movement()
    {
        transform.Translate(Time.deltaTime * _speed * Vector3.down);
        _randX = Random.Range(_MinX, _MaxX);
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(_randX, 8.0f, transform.position.z);
        }
    }
    void Firelaser()
    {
       
        if( Time.time>=_canFire )
        {
            _canFire = Time.time+ _firerate;
             Instantiate(_laserPrefab,new Vector3(transform.position.x,(transform.position.y-1.49f),transform.position.z),Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _boxCollider.enabled= false;
            _anim.SetTrigger("Destroy");
            Destroy(other.gameObject);
            AudioManager.instance.PlayAudio(_destroyClip);
            _player.score(Random.Range(10,20));
            Destroy(gameObject,2.8f);
        }
        else if(other.tag == "Player")
        {
            _anim.SetTrigger("Destroy");
            AudioManager.instance.PlayAudio(_destroyClip);
            _boxCollider.enabled = false;
            Destroy(gameObject,2.8f);
            if(_player != null)
            {
                _player.Damage();
            }
          
        }
    }

}

