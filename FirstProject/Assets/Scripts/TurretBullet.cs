using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    [SerializeField] private float _speed = 5;
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
