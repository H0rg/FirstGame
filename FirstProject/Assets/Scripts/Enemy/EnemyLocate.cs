using UnityEngine;
using UnityEngine.AI;

public class EnemyLocate : MonoBehaviour
{
    private bool _isSeePlayer = false;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Color color = Color.green;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float maxDistance = 15f;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        moveToPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isSeePlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I don't see you");
            _isSeePlayer = false;
            color = Color.green;
        }

    }

    void moveToPlayer()
    {
        if (_isSeePlayer)
        {
            RaycastHit hit;

            navMeshAgent.Stop();

            Vector3 dir = _player.position - transform.position;

            Vector3 direction = Vector3.ClampMagnitude(_player.position - transform.position, 1);


            var rayCast = Physics.Raycast(transform.position, dir, out hit, maxDistance, mask);
            if (rayCast)
            {
                if (hit.collider.transform == _player)
                {
                    print(hit.collider.name);
                    transform.LookAt(_player);
                    transform.Translate(direction * _speed * Time.deltaTime );
                    //Vector3.MoveTowards(transform.position, _player.position, _speed);
                    
                    color = Color.red;
                }
                else { print(hit.collider.name); }
            }
            Debug.DrawRay(transform.position, dir, color);
        }
        else
        {
            navMeshAgent.Resume();
        }
    }
}
