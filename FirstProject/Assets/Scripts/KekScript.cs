using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KekScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU GOT A BOX!");
            Destroy(gameObject);
        }
    }
}
