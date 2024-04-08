using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minX,_maxX;
    [SerializeField] private AudioClip _powerUpClip;
    // Start is called before the first frame update
    void Start()
    {
       transform.position = new Vector3(1,8,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.Clamp(transform.position.x, _minX, _maxX);
        transform.Translate(xPos * _speed * Time.deltaTime*Vector3.down );
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.PlayAudio(_powerUpClip);
            Destroy(gameObject);
        }
    }
}
