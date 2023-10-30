using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float _speed = 150f;
    private float _rotationX = 0;

    void Update()
    {
        Rotating();
    }
    void Rotating()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _speed; 
        _rotationX = Mathf.Clamp(_rotationX, -45, 45);
        float rotationY = transform.localEulerAngles.y;
        
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }   
}
