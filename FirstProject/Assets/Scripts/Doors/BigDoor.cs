using System;
using System.Collections;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger("Open");
    }


    #region OldVariant

    // public float openHeight = 3.9f;
    // public float duration = 3;
    // Vector3 closePosition;
    // void Start()
    // {
    //     closePosition = transform.position;
    // }
    // public void OpenDoor()
    // {
    //     Vector3 openPosition = closePosition + Vector3.up * openHeight;
    //     StartCoroutine(MoveDoor(openPosition));
    // }
    // IEnumerator MoveDoor(Vector3 targetPosition)
    // {
    //     float timeElapsed = 0;
    //     Vector3 startPosition = transform.position;
    //     while (timeElapsed < duration)
    //     {
    //         float t = Mathf.SmoothStep(0, 1, timeElapsed / duration);
    //         transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     transform.position = targetPosition;
    // }

    #endregion
}
