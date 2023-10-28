using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocate : MonoBehaviour
{
    private bool _isSeePlayer = false;
    private Transform _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Color color = Color.green;
    private NavMeshAgent _navMeshAgent;
    private Transform _parent;
    private Vector3 _lastSeenPlayerPosition;

    private float duration = 2f;

    private void Awake()
    {
        _parent = transform.parent;
        _navMeshAgent = transform.parent.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        moveToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetIsSeePlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I don't see you");
            SetIsSeePlayer();
            StartCoroutine(CheckLastSeenPosition());
        }

    }

    void moveToPlayer()
    {
        if (_isSeePlayer)
        {
            StopAllCoroutines();
            RaycastHit hit;
            _navMeshAgent.Stop();
            Vector3 direction = Vector3.ClampMagnitude(_player.position - _parent.position, 1);

            var rayCast = Physics.Raycast(_parent.position + _parent.up, direction, out hit);
            if (rayCast)
            {
                if (hit.collider.transform == _player)
                {
                    _parent.LookAt(_player);
                    if (!(Vector3.Distance(_parent.position, _player.position) < 1.5f))
                    {
                        _parent.position =
                            Vector3.MoveTowards(_parent.position, _player.position, _speed * Time.deltaTime);
                        _lastSeenPlayerPosition = _player.position;
                    }
                }
            }

        }
    }

    public IEnumerator CheckLastSeenPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = _parent.position;
        while (elapsedTime < duration)
        {
            _parent.position = Vector3.Lerp(startPosition, _lastSeenPlayerPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _navMeshAgent.Resume();
    }

    
    public void SetIsSeePlayer()
    {
        _isSeePlayer = !_isSeePlayer;
    }
}
