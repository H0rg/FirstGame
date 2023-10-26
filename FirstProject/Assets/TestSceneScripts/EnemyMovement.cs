using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    Vector3 currentVelocity;
    private float time = 1f;

    [SerializeField] float moveDuration = 5f;
    private void Start()
    {
       // transform.Translate(0, 2, 0);
    }
    void Update()
    {
        transform.Translate(0, 2f * Time.deltaTime, 0);
        //MovementToPlayer();
        if(Time.time > time)
        {
            Debug.Log($"Time = {time}; Cube position = {transform.position}");
            time += 1;
        }
    }







    

    public void MovementToPlayer()
    {
        Vector3 dir = transform.position;
        transform.position = Vector3.SmoothDamp(dir, target1.position, ref currentVelocity, speed);
        transform.LookAt(target1);
        if(Vector3.Distance(dir, target1.position) < 0.04f)
        {
            targetSpaw();
        }
    }
    void targetSpaw()
    {
        Transform temp;
        temp = target1;
        target1 = target2;
        target2 = temp;
    }
}
