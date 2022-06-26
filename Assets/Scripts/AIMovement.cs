using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    public void MoveTo(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }
    public void StopMovement()
    {
        _agent.isStopped = true;
    }
}
