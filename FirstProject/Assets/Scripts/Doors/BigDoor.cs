using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{

    private float doorX = 1.7f;
    private bool isOpen = false;
    public void Open()
    {
        transform.GetChild(0).Translate(0, 0, -doorX);
        transform.GetChild(1).Translate(0, 0, doorX);
        isOpen = true;
    }

}
