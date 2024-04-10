using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsCoOp : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector3.down*Time.deltaTime*_speed);
    }
}
