using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> SpawnPoint;
    public Enemy _prefEnemy;
    public int _enemyCounter = 0;
   
    void Start()
    {
        foreach(var spawn in SpawnPoint)
        {
            Instantiate(_prefEnemy, spawn.transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        //Spawn();
    }
    public void Spawn()
    {
        int random = Random.Range(0, SpawnPoint.Count) ;
        if(_enemyCounter < 5)
        {
            Instantiate(_prefEnemy, SpawnPoint[random].transform.position, Quaternion.identity);
        }
        _enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Count();
        Debug.Log($"{_enemyCounter} enemys around as");
    }
}
