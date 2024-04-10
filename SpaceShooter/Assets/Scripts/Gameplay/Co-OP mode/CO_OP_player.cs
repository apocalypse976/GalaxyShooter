using Unity.Netcode;
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
    [SerializeField] private NetworkObject _tripleShotPrefab;
    
    //Player Movements Vector
    private Vector2 playerInput;
    private float horizontalInput;
    private float verticalInput;
    private float _firerate=0.5f;
    private float _canfire=0;

    //Player PowerUps
    private bool _tripleShotActive=false;

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
        float horizontalMovement = _speed * Time.deltaTime * horizontalInput;
        float verticalMovement = _speed * Time.deltaTime * verticalInput;
        transform.Translate(horizontalMovement, verticalMovement, 0);
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
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
                NetworkObject _tripleShotInstance = Instantiate(_tripleShotPrefab, spawnPos, Quaternion.identity);
                _tripleShotInstance.SpawnWithOwnership(OwnerClientId);
                Invoke("DisableTripleShot", 5f);
            }
         
        }
     
    }

    #endregion
    #region Health
    public void health()
    {
        _health -= 1;
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
        }
       else if (collision.tag == "TripleShot")
       {
             Destroy(collision.gameObject);
            _tripleShotActive = true;
        }
    }
    private void DisableTripleShot()
    {
        _tripleShotActive = false;
    }
    #endregion
}
