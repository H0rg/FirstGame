using UnityEngine;

public class GhoustMove : MonoBehaviour
{
    //[SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _player;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _distance = 1.0f;
    public float _speed = 2f;
    public Vector3 startPos;
    public Vector3 dir;
    public bool rayCast;

    private Color color;
    private void FixedUpdate()
    {
        
        RaycastHit hit;
        startPos = transform.position;
        dir = _player.position - startPos;
        rayCast = Physics.Raycast(startPos, dir * 10, out hit,1);
        if (rayCast)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                color = Color.green;
                //Move();
            }
            else
            {
                color = Color.red;
            }
        }
        Debug.DrawRay(startPos, dir, color);
    }


    public void Move()
    {
        Vector3 dir = _player.position - transform.position;
        transform.Translate(dir * _speed * Time.deltaTime);
    }
}
