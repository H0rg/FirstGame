using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 1;
    private Vector3 startPosition;
    private Vector3 directionTo;
    private Vector3 directionFrom;
    private Vector3 trueDirection;
    void Start()
    {
        startPosition = transform.position;
        //direction = Vector3.ClampMagnitude(target.position - startPosition,1);
        //direction = Vector3.ClampMagnitude(startPosition - target.position,1);
        //trueDirection = Vector3.ClampMagnitude(startPosition + direction,1);
        
    }

    void Update()
    {
        movingToTarget();
        //Looking();
    }

    public void movingToTarget()
    {
        startPosition = transform.position;
        directionTo = Vector3.ClampMagnitude(target.position - startPosition, 1);
        directionFrom = Vector3.ClampMagnitude(startPosition - target.position, 1);

        //trueDirection = Vector3.ClampMagnitude(startPosition + direction,1);
        if (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            speed = 2;
            transform.Translate(directionTo * speed * Time.deltaTime);
        }

        else if (Vector3.Distance(transform.position, target.position) < 1.3f)
        {
            speed = 3;
            transform.Translate(directionFrom * speed * Time.deltaTime);
        }
    }
    public void Looking()
    {
    }
    
}
