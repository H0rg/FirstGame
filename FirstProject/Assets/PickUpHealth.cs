using UnityEngine;

public class PickUpHealth : MonoBehaviour
{
    [SerializeField] private float _heal = 5;
    [SerializeField] private float _speedRotation = 70;


    private void Update()
    {
        transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Healed!");
            other.GetComponent<PlayerMy>().Heal(_heal);
            Destroy(gameObject);
        }
    }
}
