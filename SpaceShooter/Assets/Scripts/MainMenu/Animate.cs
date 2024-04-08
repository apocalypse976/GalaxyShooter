using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _laserPrefab;
    

    private void Update()
    {
        laserMovement();
        
    }
    void laserMovement()
    {
        _laserPrefab.transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (_laserPrefab.transform.position.x > 156)
        {
            _laserPrefab.transform.position = new Vector3(-94.80f, 40, 150);
        }
    }
  
}
