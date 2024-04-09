using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _normalspeed;
    [SerializeField] private float _boostedSpeed;
    [SerializeField] private float _minX, _maxX, _minY, _maxY;

    [Header("Player Attributs")]
    [SerializeField] private float _canFire;
    [SerializeField] private int _health;


    [Header("Player Prefabs")]
    [SerializeField] private GameObject _sheildPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleshotPrefab;
    [SerializeField] private GameObject[] _thrustersPrefabs;

    [Header("Audio")]
    [SerializeField] private AudioClip _laserClip;

    private GameManager _gameManager;
    private UIManager _uiManager;
    private bool _isSheildActive;
    private bool _tripleShotActive;
    private Vector2 _playerMovement;
    private float _fireRate=0;
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _speedBoostActive;
    private int _score;


    void Start()
    {

        try
        {
            _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        }
        catch
        {
            Debug.LogError("UI Manager or Game Manager not Found.");
        }
        if (!_gameManager.IsCoOpMode)
        {
            transform.position = Vector3.zero;
        }

    

    }
    void Update()
    {
      
        movement();
       playerBound();
      


    }
   
    #region Movements
    public void Move(InputAction.CallbackContext context)
    {
        _playerMovement= context.ReadValue<Vector2>();
    }
    void movement()
    {
        if (IsOwner) Debug.Log("Is owner");
        // _horizontalMovement = Input.GetAxis("Horizontal");
        //_verticalMovement = Input.GetAxis("Vertical");
        _horizontalMovement =_playerMovement.x;
        _verticalMovement = _playerMovement.y;
       
        if (!_speedBoostActive) 
        {
            transform.Translate(_horizontalMovement * _normalspeed * Time.deltaTime, _verticalMovement * _normalspeed * Time.deltaTime, 0);
        }
        else if (_speedBoostActive)
        {
            transform.Translate(_horizontalMovement * _boostedSpeed * Time.deltaTime, _verticalMovement * _boostedSpeed * Time.deltaTime, 0);
        }
       
    }
    void playerBound()
    {
        if(transform.position.x>_maxX)
        {
            transform.position = new Vector3(_minX, transform.position.y, transform.position.z);
        }
        else if(transform.position.x<_minX)
        {
            transform.position = new Vector3(_maxX, transform.position.y, transform.position.z);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY), transform.position.z);
    }
    #endregion

    #region Ammo
    public void laser()
    {
        if (Time.time > _fireRate)
        {
            if (_tripleShotActive)
             {
                _fireRate = Time.time + _canFire;
                Instantiate(_tripleshotPrefab,transform.position+new Vector3(-1.15f,1.2f,0),Quaternion.identity);
                Invoke("tripleShot", 5.0f);
            }
           else if(!_tripleShotActive)
            {
                _fireRate = Time.time + _canFire;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.45f, 0), Quaternion.identity);
            }
          
        }
        AudioManager.instance.PlayAudio(_laserClip);
    }
    void tripleShot()
    {
        _tripleShotActive = false; ;
    }
    #endregion

    #region Health
    public void Damage()
    {
        if (!_isSheildActive)
        {
            _health -= 1 ;
            if(_health == 2)
            {
                _uiManager.UpdateLives(_health);
                _thrustersPrefabs[0].SetActive(true);
            }
            else if(_health == 1 )
            {
                _uiManager.UpdateLives(_health);
                _thrustersPrefabs[1].SetActive(true);
            }
            else if (_health == 0)
            {
                _uiManager.UpdateLives(_health);
                Destroy(gameObject);
            }

        }
        else if (_isSheildActive)
        {
            _isSheildActive = false;
            _sheildPrefab.SetActive(false);
            return;
           
        }
       
        
    }
    #endregion

    #region PowerUps

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUps")
        {
            
            _tripleShotActive = true;
        }
        else if (collision.tag == "SpeedBoost")
        {
            StartCoroutine(speedBoostRoutine());
        
        }
        else if(collision.tag == "SheildPowerUps")
        {
            _isSheildActive = true;
            _sheildPrefab.SetActive(true);
        }
    }
    IEnumerator speedBoostRoutine()
    {
        _speedBoostActive = true;
        yield return new WaitForSeconds(5);
        _speedBoostActive = false;
    }
    #endregion

    #region Score
    public void score(int _points)
    {
        _score += _points;
        _uiManager.UpdateScore(_score);
    }
    #endregion

}