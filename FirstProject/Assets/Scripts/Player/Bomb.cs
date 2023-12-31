using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _explosionTime = 3f;
    private float _explosionDamage = 10f;
    private float _explosionPower = 15f;
    
    private void Start()
    {
        Invoke("Explosion", _explosionTime);

    }
    private void Explosion()
    {
        var collisions = Physics.OverlapSphere(transform.position, 5);
        foreach (var collision in collisions)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(_explosionDamage);
            }
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                var tf = collision.gameObject.GetComponent<Transform>();
                var rb = collision.gameObject.GetComponent<Rigidbody>();

                var newDir = Vector3.RotateTowards(transform.forward, tf.position - transform.position,2,2f);

                rb.AddForce(newDir * _explosionPower, ForceMode.Impulse);
            }

        }
        Destroy(gameObject);
    }
}
