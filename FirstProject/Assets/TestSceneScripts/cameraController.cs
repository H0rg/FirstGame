using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
     Vector3 dir;
    [SerializeField] private float sensivity = 5f;
    Vector3 _rotation;
    
    void Update()
    {
        //transform.LookAt(target);
        //Move();
    }

    public void Move()
    {
        _rotation.x = Input.GetAxis("Mouse X"); 
        _rotation.z = Input.GetAxis("Mouse Y");

        transform.eulerAngles = _rotation * sensivity;

    }
}

