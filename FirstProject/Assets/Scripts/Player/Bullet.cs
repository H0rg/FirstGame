using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 4;

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pey");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit you)");
            other.GetComponent<Enemy>().TakeDamage();
        }
        Destroy(gameObject);
    }

}