using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private float _explosionRadius = 5f;
    private float _explosionPower = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Explosion();
    }
    public void Explosion()
    {
        Debug.Log("Explosion");

        var collisions = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var item in collisions)
        {

            var go = item.gameObject.GetComponent<Transform>();
            if(go.GetComponent<Rigidbody>() != null)
            {
                //go.GetComponent<Rigidbody>().AddForce(go.transform.position - transform.position);

                var tf = item.gameObject.GetComponent<Transform>();
                var rb = item.gameObject.GetComponent<Rigidbody>();

                var newDir = Vector3.RotateTowards(transform.forward, go.transform.position - transform.position, 2, 2f);

                rb.AddForce(newDir * _explosionPower, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
