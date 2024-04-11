using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CO_OP_player : NetworkBehaviour
{
    [Header("Player Movements")]
    [SerializeField] private float _speed;

    [Header("Player Bounds")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [Header("Laser")]
    [SerializeField] private NetworkObject _laserPrefab;

    [Header("Health")]
    [SerializeField] private int _health=3;

    [Header("PowerUps Prefab")]
    [SerializeField] private GameObject _sheildPrefab;
    
    //Player Movements Vector
    private Vector2 playerInput;
    private float horizontalInput;
    private float verticalInput;
    private float _firerate=0.5f;
    private float _canfire=0;

    //Player PowerUps
    private bool _tripleShotActive=false;
    private bool _speedBoost = false;
    private bool _isSheildBoost = false;
    private float _speedMultiplier=2f;

    void Update()
    {
        if (!IsOwner) return;
        Movement();
        playerBounds();
        if (Input.GetMouseButtonDown(0) )
        {
            SpawnLaserServerRpc();
        }
           
    }
    #region Move
    public void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }
    void Movement()
    {
        horizontalInput = playerInput.x;
        verticalInput = playerInput.y;
        float horizontalMovement = _speed * Time.deltaTime* horizontalInput;
        float verticalMovement = _speed * Time.deltaTime* verticalInput;
        if (_speedBoost)
        { 
            transform.Translate(horizontalMovement * _speedMultiplier , verticalMovement * _speedMultiplier , 0);
            Invoke("DisableSpeedBoost", 5f);
        }
        else if (!_speedBoost)
        {
            transform.Translate(horizontalMovement, verticalMovement, 0);
        }
      
    }
    void playerBounds()
    {
        if (transform.position.x > _maxX)
        {
            transform.position = new Vector3(_minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _minX)
        {
            transform.position = new Vector3(_maxX, transform.position.y, transform.position.z);
        }
        float playerBoundsY = Mathf.Clamp(transform.position.y, _minY, _maxY);
        transform.position = new Vector3(transform.position.x, playerBoundsY, transform.position.z);
    }
    #endregion
    #region Laser

    [ServerRpc]
    public void SpawnLaserServerRpc()
    {
        if (Time.time > _canfire)
        {
            if (!_tripleShotActive)
            {
                _canfire = Time.time + _firerate;
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
                NetworkObject laserInstance = Instantiate(_laserPrefab, spawnPos, Quaternion.identity);
                laserInstance.SpawnWithOwnership(OwnerClientId);
            }
            else if (_tripleShotActive)
            {
                _canfire = Time.time + _firerate;
                //Up Laser
                Vector3 spawnPos1 = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
                NetworkObject _tripleShotInstance = Instantiate(_laserPrefab, spawnPos1, Quaternion.identity);
                _tripleShotInstance.SpawnWithOwnership(OwnerClientId);
                //Right Laser
                Vector3 spawnPos2 = new Vector3(transform.position.x-0.8f, transform.position.y, transform.position.z);
                NetworkObject _tripleShotInstance1 = Instantiate(_laserPrefab, spawnPos2, Quaternion.identity);
                _tripleShotInstance1.SpawnWithOwnership(OwnerClientId);
                //Left Laser
                Vector3 spawnPos3 = new Vector3(transform.position.x+0.8f, transform.position.y, transform.position.z);
                NetworkObject _tripleShotInstance3 = Instantiate(_laserPrefab, spawnPos3, Quaternion.identity);
                _tripleShotInstance3.SpawnWithOwnership(OwnerClientId);
                Invoke("DisableTripleShot", 5f);
            }
         
        }
     
    }

    #endregion
    #region Health
    public void health()
    {
        if (_isSheildBoost)
        {
            return;
        }
        else if (!_isSheildBoost)
        {
            _health -= 1;
        }
        
      if(_health==0)
      {
         NetworkObject.Despawn();
      }
    }

    #endregion
    #region Triggers and PowerUps
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Enemy" || collision.tag == "Enemy_laser")
        {
            health();
            if (_isSheildBoost)
            {
                if (!IsOwner) return;
                DisableSheildServerRpc();
            }
        }
       else if (collision.tag == "TripleShot")
       {
             Destroy(collision.gameObject);
            _tripleShotActive = true;
        }
        else if (collision.tag == "SpeedBoost")
        {
            Destroy(collision.gameObject);
            _speedBoost = true;
        }
        else if (collision.tag == "SheildPowerUps")
        {
            if (!IsOwner) return;
            EnebleSheildServerRpc();
            Destroy(collision.gameObject);
        }
    }
    private void DisableTripleShot()
    {
        _tripleShotActive = false;
    }
    private void DisableSpeedBoost()
    {
        _speedBoost = false;
    }
    [ServerRpc]
    void EnebleSheildServerRpc()
    {
       
     _isSheildBoost = true;
        _sheildPrefab.SetActive(true);
       
    }
    [ServerRpc]
    void DisableSheildServerRpc()
    {
        _isSheildBoost = false;
        _sheildPrefab.SetActive(false);
    }
    #endregion
}
