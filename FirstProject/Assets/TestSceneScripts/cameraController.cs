using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform target; 
    Vector3 dir;
    [SerializeField] private float sensivity = 1f;
    Vector3 _rotation;
    
    void Update()
    {
        //transform.LookAt(target);
        Move();
    }

    public void Move()
    {
        _rotation.y = Input.GetAxis("Mouse X");
        
        transform.Rotate(_rotation * sensivity);
    }
}

