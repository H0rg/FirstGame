using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;

    private float _maxHp = 10;
    private float _currentHp;
    public bool _IsAlive { get; private set; }

    [SerializeField] private Transform _graveStonePosition;
    [SerializeField] private GameObject _prepGraveStone;
    [SerializeField] private MeshRenderer _enemyRender;
    [SerializeField] private Color _deathColor;

    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex;

    void Awake()
    {
        _IsAlive = true;
        _currentHp = _maxHp;
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        if (wayPoints.Count > 0 && wayPoints[0] != null)
        {
            navMeshAgent.SetDestination(wayPoints[0].position);
        }
    }
    private void Update()
    {
        moveToWayPoints();
    }
    
    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0 && _IsAlive == true)
        {
            _IsAlive = false;
            StartCoroutine(DeadAnimation());
        }

    }
    public void Die()
    {
        Instantiate(_prepGraveStone, _graveStonePosition.position, transform.rotation);
        Destroy(gameObject);
    }
    IEnumerator DeadAnimation()
    {
        navMeshAgent.Stop();
        while (_enemyRender.material.color != _deathColor)
        {
            _enemyRender.material.color = Color.Lerp(_enemyRender.material.color, _deathColor, 0.03f);
            yield return null; 
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void moveToWayPoints()
    {
        if (wayPoints.Count > 0 && wayPoints[0] != null)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                _currentWayPointIndex = (_currentWayPointIndex + 1) % wayPoints.Count;
                navMeshAgent.SetDestination(wayPoints[_currentWayPointIndex].position);
            }
        }
    }


}
