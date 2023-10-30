using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COCA COLA");
            other.GetComponent<PlayerMy>().ColaMove();
            Destroy(gameObject);
        }
    }
}
