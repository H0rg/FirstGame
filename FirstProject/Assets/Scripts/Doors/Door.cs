using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float _rotation = -80f;
    public bool _isOpen = false;
    public void OpenDoor()
    {
        Debug.Log("Door Open");
        transform.Rotate(0, _rotation, 0);
        _isOpen = true;
    }
    public void CloseDoor()
    {
        Debug.Log("Door Close");
        transform.Rotate(0, -_rotation, 0);
        _isOpen = false;
    }

}
