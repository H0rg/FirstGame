using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    private float damage = 5;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        other.GetComponent<Enemy>().TakeDamage(damage);
    //    }
    //    Destroy(gameObject);
    //}

}