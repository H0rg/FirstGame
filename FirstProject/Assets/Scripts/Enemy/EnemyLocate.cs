using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocate : MonoBehaviour
{
    private bool _isSeePlayer = false;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed =0.5f;
    [SerializeField] private Color color = Color.green;
    private NavMeshAgent navMeshAgent;
    public LayerMask mask;

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

            Vector3 dir = _player.position - transform.position + Vector3.up;
            Vector3 rot = _player.position - transform.position;

            var rayCast = Physics.Raycast(transform.position, dir, out hit,10, ~(1<<8));
            if (rayCast)
            {
                if (hit.collider.transform == _player)
                {
                    transform.rotation = Quaternion.LookRotation(rot);
                    transform.Translate((_player.position - transform.position) * _speed * Time.deltaTime);
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
