using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2,
    }

    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;
    [SerializeField] private float sensitivityHor = 1.5f;
    [SerializeField] private float sensitivityVer = 1.5f;

    private float _rotationY;
    private float _rotationX;

    private float minVert = -45f;
    private float maxVert = 45f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            
            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else
        {

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            
            _rotationY += Input.GetAxis("Mouse X") * sensitivityHor;


            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }

}
