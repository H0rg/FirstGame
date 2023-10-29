using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoorButton : MonoBehaviour
{
    [SerializeField] private bool KeyOne = false;
    [SerializeField] private bool KeyTwo = false;
    [SerializeField] private GameObject bigDoor;
    private BigDoor bigDoorScript;
    void Awake()
    {
        bigDoorScript = bigDoor.GetComponent<BigDoor>();
    }

    void Update()
    {
        if (KeyOne == true && KeyTwo == true)
        {
            Debug.Log("Openning");
            bigDoorScript.OpenDoor();
            KeyOne = false;
            KeyTwo = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMy>().keyOne == true)
            {
                transform.Find("KeyOne").gameObject.SetActive(true);
                KeyOne = true;
            }
            if (other.GetComponent<PlayerMy>().keyTwo == true)
            {
                transform.Find("KeyTwo").gameObject.SetActive(true);
                KeyTwo = true;
            }
        }
    }
}
