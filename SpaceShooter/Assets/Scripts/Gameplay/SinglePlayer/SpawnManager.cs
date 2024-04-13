using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _astroidPrefab;
    [SerializeField] private GameObject _player1;
    bool playeralive =true;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i< _powerUps.Length; i++)
        {
            if (_powerUps[i] == null)
            {
                Debug.LogError("Please attach Power Ups prefab.");
            }
        }
        if (!_player1 || !_enemyPrefab)
        {
            Debug.LogError("Please attach, Player prefab or Enemy Prefab or Enemy Container");
        }
       
        Instantiate(_astroidPrefab, new Vector3(Random.Range(-9.1f, 9.8f), Random.Range(1,6), 0), Quaternion.identity);

    }
    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(_spawnPoweUpsRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_player1 == null)
        {
            playeralive = false;
        }
        
       
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (playeralive)
        {
            Vector3 Spawnpos= new Vector3(Random.Range(-9.1f,9.8f),7.8f,0);
            Instantiate(_enemyPrefab, Spawnpos, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            
        }

    }
   
    IEnumerator _spawnPoweUpsRoutine()
    {
        yield return new WaitForSeconds(Random.Range(2, 10));
        while (playeralive)
        {
            int index = Random.Range(0, _powerUps.Length);
            Vector3 Spawnpos = new Vector3(Random.Range(-9.1f, 9.8f), 7.8f, 0);
            Instantiate(_powerUps[index], Spawnpos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }
}
