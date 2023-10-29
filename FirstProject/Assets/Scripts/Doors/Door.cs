using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float _rotation = -80f;
    public bool _isOpen = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        _animator.SetBool("_isOpen",!_isOpen);
        _animator.SetTrigger("Open");
        _isOpen = true;
    }
    public void CloseDoor()
    {
        _animator.SetTrigger("Close");
        _isOpen = false;
    }
}
