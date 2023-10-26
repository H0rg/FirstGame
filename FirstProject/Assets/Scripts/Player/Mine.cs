using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private float _explosionRadius = 5f;
    private float _explosionPower = 10f;
    private float _explosionDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Explosion();
    }
    public void Explosion()
    {
        var collisions = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var item in collisions)
        {
            if(item.GetComponent<Rigidbody>() != null)
            {
                var tf = item.gameObject.GetComponent<Transform>();
                var rb = item.gameObject.GetComponent<Rigidbody>();

                var newDir = Vector3.RotateTowards(transform.forward, item.transform.position - transform.position, 2, 2f);

                rb.AddForce(newDir * _explosionPower, ForceMode.Impulse);
            }
            if (item.CompareTag("Enemy"))
            {
                item.GetComponent<Enemy>().TakeDamage(_explosionDamage);
            }
        }
        Destroy(gameObject);
    }
}
