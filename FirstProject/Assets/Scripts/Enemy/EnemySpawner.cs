using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> SpawnPoint;
    public Enemy _prefEnemy;
    public int _enemyCounter = 0;

    private float _spawnTime = 2;
    private float _nextSpawnTime = 5;

    private int _maxEnemy = 3;
    private int _currentEnemy = 0;


    void Awake()
    {
        Invoke("SpawnEnemy", _spawnTime);
        //InvokeRepeating("SpawnEnemy", _spawnTime, _nextSpawnTime);
    }

    private void Update()
    {
    }
    public void SpawnEnemy()
    {
        Instantiate(_prefEnemy, SpawnPoint[0].transform.position, Quaternion.identity);
        _currentEnemy++;
        //if(_currentEnemy >= _maxEnemy)
        //{
        //    CancelInvoke();
        //    print("Invoke have been canceled");
        //}
    }
}
