using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private int _hp = 2;
    private int _time = 3;

    public int Hp { get { return _hp; } }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void TakeDamage()
    {
        _hp--;
        if (_hp <= 0)
        {
            Die();
        }
        Debug.Log($"I got shot. HP = {Hp} ");

    }
    public void Die()
    {

        Destroy(gameObject);

    }
}
