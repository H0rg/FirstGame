using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    //[SerializeField] private bool _isOpen = false;
    private float duration = 5f;
    private Vector3 closePosition;
    private Vector3 openPosition;
    private float openHeight = 3.7f;


    private void Start()
    {
        closePosition = transform.position;
    }

    public void OpenDoor()
    {
        Vector3 openPos = closePosition + Vector3.up * openHeight;
        StopAllCoroutines();
        StartCoroutine(Opening(openPos));
    }
    public void CloseDoor()
    {
        Vector3 closePos = closePosition;
        StopAllCoroutines();
        StartCoroutine(Closing(closePos));
    }

     IEnumerator Opening(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, target,  t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }

     IEnumerator Closing(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, target, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}
