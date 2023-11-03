using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    private float _maxHp = 10;
    private float _currentHp;
    public bool _IsAlive { get; private set; }

    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex;
    
    private Rigidbody rb;
    private Animator _animator;
    private Transform pointOfView;
    private Slider _slider;
    private ParticleSystem _particleSystem;

    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        
        _slider = transform.Find("CanvasHP").GetComponent<Slider>();
        
        pointOfView = transform.Find("PointOfView");
        _IsAlive = true;
        _slider.maxValue = _maxHp; 
        _currentHp = _maxHp;
        _slider.value = _currentHp;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
        _particleSystem.Play();
        _currentHp -= damage;
        if (_currentHp <= 0 && _IsAlive == true)
        {
            _IsAlive = false;
            StartCoroutine(DeadAnimation());
        }
        _slider.value = _currentHp;

    }
    private IEnumerator DeadAnimation()
    {
        navMeshAgent.Stop();
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        pointOfView.gameObject.SetActive(false);
        
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(3);
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
