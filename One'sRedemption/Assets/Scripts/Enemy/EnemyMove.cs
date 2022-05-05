using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove
{
    float _distance;
    int _indexPoint;
    protected NavMeshAgent agent;
    protected Transform[] waypoints;
    protected LayerMask playerMask;
    protected float range;
    protected bool vision;
    protected Transform target;

    public EnemyMove(float range, Transform target, NavMeshAgent agent)
    {
        this.range = range;
        this.target = target;
        this.agent = agent;
    }

    public void EnemyBehaviour(Transform entityTransform, bool canMove)
    {
        vision = Physics.CheckSphere(entityTransform.position, range);

        if (vision == true && canMove)
        {
            MoveToTarget();
        }
        //if (vision == false)
        //{
        //    _distance = Vector3.Distance(transform.position, waypoints[_indexPoint].position);
        //    Patrol();
        //}
        //if (_distance <= 0.7)
        //{
        //    _indexPoint++;
        //
        //}
        //if (_indexPoint >= waypoints.Length)
        //{
        //    _indexPoint = 0;
        //}
    }

    //public void Patrol()
    //{
    //    agent.SetDestination(waypoints[_indexPoint].position);
    //}
    public void MoveToTarget()
    {
        if (agent != null)
        {
            agent.SetDestination(target.position);
        }
        else Debug.Log("asd");
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}
}
