using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOne : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var flag = other.GetComponent<PlayerMy>();
            flag.keyOne = true;
            Debug.Log("You got Key One");
            Destroy(gameObject);
        }
    }
}
