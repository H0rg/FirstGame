using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _time;
    void Start()
    {
        var enemyGO = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemyGO)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
        }
    }

    void Update()
    {
        
    }
    public void Init(float time)
    {
        _time = time;
        Destroy(gameObject,_time);
    }
}
