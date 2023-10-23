using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision Stay");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
    }
}
