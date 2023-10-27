using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{

    [SerializeField] private float distance = 1;
    [SerializeField] private Transform target;
    private Vector3 direction;
    private Vector3 startPosition;
    private Vector3 trueDirection;
    private float timeElapse = 0;
    private float duration = 2.5f;
    private bool flag = false;
    
    void Start()
    {
        startPosition = transform.position;
        direction = startPosition - target.position;
        //direction = (target.position - startPosition) ;
   
        trueDirection = startPosition + direction;
        
        StartCoroutine(Moving());
    }
    

    public IEnumerator Moving()
    {
        flag = true;
        while (timeElapse < duration)
        {
            transform.position = Vector3.Lerp(startPosition, trueDirection, timeElapse / duration);
            timeElapse += Time.deltaTime;
            yield return null;
        }
        transform.position = trueDirection;
    }
}
