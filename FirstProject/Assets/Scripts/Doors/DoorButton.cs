using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
     public Door _door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(_door._isOpen == false)
                _door.OpenDoor();
            else _door.CloseDoor();
        }
    }
}
