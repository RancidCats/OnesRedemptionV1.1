using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove
{
    protected NavMeshAgent agent;
    protected Transform target;
    protected LayerMask layer;

    public EnemyMove(Transform target, NavMeshAgent agent)
    {
        this.target = target;
        this.agent = agent;
    }

    public void EnemyBehaviour(bool canMove)
    {
        if (canMove)
        {
            MoveToTarget();
        }
    }
    public void MoveToTarget()
    {
        if (agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
