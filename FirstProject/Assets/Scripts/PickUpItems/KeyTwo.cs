using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTwo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var flag = other.GetComponent<PlayerMy>();
            flag.keyTwo = true;
            Debug.Log("You got Key Two");
            Destroy(gameObject);
        }
    }
}
