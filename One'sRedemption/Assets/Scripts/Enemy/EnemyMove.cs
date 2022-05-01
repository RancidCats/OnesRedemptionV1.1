using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
    float _distance;
    int _indexPoint;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public LayerMask playerMask;
    public float range;
    public bool vision;
    public Transform player;

    void Update()
    {
        vision = Physics.CheckSphere(transform.position, range, playerMask);

        if (vision == true)
        {
            MoveToTarget();
        }
        if (vision == false)
        {
            _distance = Vector3.Distance(transform.position, waypoints[_indexPoint].position);
            Patrol();
        }
        if (_distance <= 0.7)
        {
            _indexPoint++;

        }
        if (_indexPoint >= waypoints.Length)
        {
            _indexPoint = 0;
        }
    }

    public void Patrol()
    {
        agent.SetDestination(waypoints[_indexPoint].position);
    }
    public void MoveToTarget()
    {
        agent.SetDestination(player.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
