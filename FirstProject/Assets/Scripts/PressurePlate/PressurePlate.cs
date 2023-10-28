using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PressurePlate : MonoBehaviour
{
    private Transform buttonPosition;
    [SerializeField] private GameObject SecretDoor;
    private SecretDoor scripsDoor;

    private void Start()
    {
        scripsDoor = SecretDoor.GetComponent<SecretDoor>();
        buttonPosition = transform.Find("Button").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HeavyItem"))
        {
            scripsDoor.OpenDoor();
            Debug.Log("MovingDown");
            buttonPosition.Translate(0, -0.09f, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HeavyItem"))
        {
            scripsDoor.CloseDoor();
            Debug.Log("MovingUp");
            buttonPosition.Translate(0, 0.09f, 0);
        }
    }
}
