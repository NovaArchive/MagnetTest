using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float sampleDistance = 10f;

    private EnemyManager _manager;
    
    private void Update()
    {
        if (GameManager.Instance.CurrentPhase != GameManager.GamePhase.InGame) return;
        
        if (!(agent.remainingDistance <= agent.stoppingDistance)) return;
        if (!NavigationExtension.GetRandomPoint(transform.position, sampleDistance, out Vector3 point)) return;
        
        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
        agent.SetDestination(point);
    }

    public void Init(EnemyManager manager)
    {
        _manager = manager;
    }

    public void OnKilled()
    {
        _manager.OnEnemyKilled();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sampleDistance);
    }
}
