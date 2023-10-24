using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    private float damage = 5;

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        if(other.name != "Bomb(Clone)")
            Destroy(gameObject);

    }

}