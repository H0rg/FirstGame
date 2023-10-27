using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    
    void Update()
    {
        Vector3 newPos = new Vector3(0,10,0);
        
        transform.Rotate(newPos * Time.deltaTime);
    }
}
