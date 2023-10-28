using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyLocate : MonoBehaviour
{
    private bool _isSeePlayer = false;
    private Transform _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Color color = Color.green;
    private NavMeshAgent _navMeshAgent;
    private Transform _parent;
    private Vector3 _lastSeenPlayerPosition;
    [SerializeField] private float _damage = 1f;
    private bool _makeHit = false;
    private float _timeBetweenHits = 3f;
    public float elapsedTime = 0;
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
        if(_makeHit)
            HitsReload();
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
                    else if (_makeHit == false)
                    {
                        _player.gameObject.GetComponent<PlayerMy>().TakeDamage(_damage);
                        _makeHit = true;
                        Debug.Log($"Player HP = [{_player.gameObject.GetComponent<PlayerMy>()._currentHp}] ");
                    }
                }
            }

        }
    }

    public void HitsReload()
    {
        if (elapsedTime < _timeBetweenHits)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            elapsedTime = 0;
            _makeHit = false;
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
