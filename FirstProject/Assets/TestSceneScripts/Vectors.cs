using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float _speed = 5f;


    private void Start()
    {
    }


    void Update()
    {

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = Vector3.ClampMagnitude(dir, 1);
        transform.Translate(Vector3.ClampMagnitude(dir, 1) * _speed * Time.deltaTime);


        //draw();
        //moveByTranslate();
    }

    public void draw()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            color = Color.blue;
            Debug.DrawRay(transform.position, Vector3.back, color, 5);
            Debug.Log("Vector3.back  -- axis Z");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            color = Color.blue;
            Debug.DrawRay(transform.position, Vector3.forward, color, 5);
            Debug.Log("Vector3.forward  -- axis Z");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            color = Color.green;
            Debug.DrawRay(transform.position, Vector3.down, color, 5);
            Debug.Log("Vector3.down  -- axis Y");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            color = Color.green;
            Debug.DrawRay(transform.position, Vector3.up, color, 5);
            Debug.Log("Vector3.up  -- axis Y");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            color = Color.red;
            Debug.DrawRay(transform.position, Vector3.right, color, 5);
            Debug.Log("Vector3.right  -- axis X");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            color = Color.red;
            Debug.DrawRay(transform.position, Vector3.left, color, 5);
            Debug.Log("Vector3.left  -- axis X");
        }

    }
    public void moveByTranslate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
            //transform.Translate(-transform.forward * _speed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            //transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            //transform.Translate(-transform.up * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            //transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            //transform.Translate(transform.right * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            //transform.Translate(-transform.right * _speed * Time.deltaTime, Space.World);

        }
    }
}
