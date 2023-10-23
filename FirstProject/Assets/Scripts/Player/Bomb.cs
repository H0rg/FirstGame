using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    private List<Enemy> _enemys = new List<Enemy>();
    private float _explosionTime = 100;
    private float _damage = 8;
    public void Init(float time)
    {
        _explosionTime = time;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           // _enemys.Add(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //_enemys.Remove(other.GetComponent<Enemy>());
        }
    }
    private void Update()
    {
        _explosionTime -= Time.deltaTime;
        if(_explosionTime <= 0)
        {
            Explosion();
        }
    }
    private void Explosion()
    {
        foreach (Enemy enemy in _enemys)
        {
            enemy.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
     
    
}
