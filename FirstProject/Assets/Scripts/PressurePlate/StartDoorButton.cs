using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorButton : MonoBehaviour
{
    private Transform buttonPosition;
    [SerializeField] private GameObject SecretDoor;
    private SecretDoor scripsDoor;
    private bool _isOpen = false;

    private void Start()
    {
        scripsDoor = SecretDoor.GetComponent<SecretDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isOpen)
            {
                scripsDoor.OpenDoor();
                _isOpen = true;
            }
            else
            {
                scripsDoor.CloseDoor();
                _isOpen = false;
            }
            
        }
    }
}
