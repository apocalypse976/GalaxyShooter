using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed;
  

   

    void Update()
    { 
       MoveUp();
        destroyLaser();
    }
  
    void MoveUp()
    {

        transform.Translate(Time.deltaTime * Vector3.up * _speed);
    }

    void destroyLaser()
    {
        if (transform.position.y > 11f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


}
