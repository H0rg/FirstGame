using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private float _maxHp = 10;
    private float _currentHp;
    [SerializeField] private Transform _graveStonePosition;
    [SerializeField] private GameObject _prepGraveStone;

    void Awake()
    {

        _currentHp = _maxHp;
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navMeshAgent.SetDestination(_target.position );
        
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
}
