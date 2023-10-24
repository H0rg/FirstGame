using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;

    private float _maxHp = 10;
    private float _currentHp;

    [SerializeField] private Transform _graveStonePosition;
    [SerializeField] private GameObject _prepGraveStone;

    private float _time = 0;
    private float _interval = 3f;

    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex;

    void Awake()
    {
        _currentHp = _maxHp;
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
    }
    private void Update()
    {
        moveToWayPoints();
    }
    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
        Debug.Log($"I got shot. HP = {_currentHp} ");

    }
    public void Die()
    {
        Instantiate(_prepGraveStone, _graveStonePosition.position, transform.rotation);
        Destroy(gameObject);
    }
    public void moveToWayPoints()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            _currentWayPointIndex = (_currentWayPointIndex + 1) % wayPoints.Count;
            navMeshAgent.SetDestination(wayPoints[_currentWayPointIndex].position);
        }
    }

}
