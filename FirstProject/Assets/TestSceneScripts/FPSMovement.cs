using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private float dirX;
    private float dirZ;
    private CharacterController _charController;
    private float gravity = -9.8f;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        dirX = Input.GetAxis("Horizontal") * _speed;
        dirZ = Input.GetAxis("Vertical") * _speed;
        Vector3 movement = new Vector3(dirX, 0, dirZ);
        movement = Vector3.ClampMagnitude(movement,_speed);
        
        movement.y = gravity;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
